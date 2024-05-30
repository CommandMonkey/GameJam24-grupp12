using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] Transform playerGraphics;
    [SerializeField] GameObject doubleJumpVFX;
    [SerializeField] Animator animator;
 
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Collider2D groundCheck;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] int jumpAmount = 2;
    [SerializeField] float startGravityDecreaseAfter = 10f;
    [SerializeField, Range(0f, 1f)] float gravityScaleDecrease = 1f;
    [SerializeField, Min(0.1f)] float minGravity = 0.1f;
    [SerializeField] float maxSpeed = 1f;

    [Header("Shooting")]
    [SerializeField] GameObject StarBulletPrefab;
    [SerializeField] float bulletSpeed;

    // Priv Vars
    Vector2 moveInput;
    int jumpsLeft;
    LayerMask groundCheckMask;
    Vector2 mousePos;
    float defaultGravityScale;

    // Priv Stats
    int wateringCans;
    Cursor cursor;
    Rigidbody2D rigidbody2d;


    public PlayerInput playerInput;
    InputAction descendAction;


    void Start()
    {
        cursor = FindObjectOfType<Cursor>();
        playerInput = GetComponent<PlayerInput>();

        rigidbody2d = GetComponent<Rigidbody2D>();
        groundCheckMask = LayerMask.GetMask("Ground");
        defaultGravityScale = rigidbody2d.gravityScale;

        descendAction = playerInput.actions["Descend"];
    }

    void Update()
    {
        ResetJumpCheck();
        CalculateGravityScale();
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (rigidbody2d.velocity.x > 0)
        {
            playerGraphics.rotation = Quaternion.Euler(0f, 180f, 0f);
        } 
        else if (rigidbody2d.velocity.x < 0)
        {
            playerGraphics.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void FixedUpdate()
    {
        Run();
    }
    bool DownPressed = false;
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        DownPressed = moveInput.y < 0 ? true : false;
    }

    void OnDescend(InputValue value)
    { 
            Debug.Log("ihoierjgh");
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
    }

    void OnJump(InputValue value)
    {
        if (!groundCheck.IsTouchingLayers(groundCheckMask) && jumpsLeft <= 0) return;
        if (DownPressed)
        {
            OnDescend(new InputValue());
        }
        else // Jump
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpHeight);
            PlayDoubleJumpVFX();
            jumpsLeft--;
        }
    }

    void PlayDoubleJumpVFX()
    {
        if(groundCheck.IsTouchingLayers(groundCheckMask)) { return; }
        Vector2 spawnPosition = new Vector2(
            transform.position.x, 
            transform.position.y - transform.localScale.y / 2);
        GameObject jumpVFX = Instantiate(doubleJumpVFX, spawnPosition, Quaternion.identity);
        Destroy(jumpVFX, 2f);
    }

    void Run()
    {
        bool playerIsMoving = Mathf.Abs(rigidbody2d.velocity.x) > Mathf.Epsilon;
        float moveX = moveInput.x * moveSpeed;
        rigidbody2d.velocity = new Vector2(
            Mathf.Clamp(moveX + rigidbody2d.velocity.x, -maxSpeed, maxSpeed), 
            Mathf.Clamp(rigidbody2d.velocity.y, -maxSpeed, maxSpeed));

        animator.SetBool("IsWalking", true);
    }

    void ResetJumpCheck()
    {
        if (groundCheck.IsTouchingLayers(groundCheckMask))
        {
            jumpsLeft = jumpAmount;
        }
    }

    void OnShootStar()
    {
        if (cursor == null)
        {
            Debug.LogWarning("Needs A Cursor Object To Shoot, cant find any in the scene!!");
            return;
        }
        Vector2 direction = cursor.transform.position - transform.position;
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        Quaternion StarBulletRotation = Quaternion.Euler(0, 0, angleInDegrees);
        GameObject starInstance = Instantiate(StarBulletPrefab, transform.position, StarBulletRotation);

        starInstance.GetComponent<Rigidbody2D>().velocity = starInstance.transform.right * bulletSpeed;
        Destroy(starInstance, 3f);
    }

    void CalculateGravityScale()
    {
        if (transform.position.y < startGravityDecreaseAfter) return;
        float currentGravityScale;

        currentGravityScale = defaultGravityScale * Mathf.Pow(gravityScaleDecrease, Mathf.Min(transform.position.y - startGravityDecreaseAfter, 1f));
        if (currentGravityScale < minGravity) 
        { 
            currentGravityScale = minGravity; 
        }
        rigidbody2d.gravityScale = currentGravityScale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckForKeyInteractCollectables(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckForWalkIntoCollectables(collision.gameObject);
    }

    private void CheckForKeyInteractCollectables(GameObject colobject)
    {
        Collectible collectable = colobject.GetComponent<Collectible>();
        if (collectable != null)
        {
            if (collectable.interactType == Collectible.Type.keyInterract)
            {
                collectable.SetCollectTextActive(true);
            }
        }
    }
    private void CheckForWalkIntoCollectables(GameObject colobject)
    {
        Collectible collectable = colobject.GetComponent<Collectible>();
        if (collectable != null)
        {
            if (collectable.interactType == Collectible.Type.walkInto)
            {
                collectable.Collect();
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Collectible collectable = collision.gameObject.GetComponent<Collectible>();
        if (collectable != null)
        {
            if (collectable.interactType == Collectible.Type.keyInterract)
            {
                collectable.SetCollectTextActive(false);
            }
        }
    }

    void OnInterract()
    {

    }

    internal bool AddWateringCan(int value)
    {
        if (wateringCans + value < 0)
        {
            return false;
        }
        else
        {
            wateringCans += value;
            UpdateWateringCanUI();
            return true;
        }


    }

    private void UpdateWateringCanUI()
    {
        throw new NotImplementedException();
    }
}
