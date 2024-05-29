using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] Transform playerGraphics;

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


    void Start()
    {
        cursor = FindObjectOfType<Cursor>();
        playerInput = GetComponent<PlayerInput>();

        rigidbody2d = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<Collider2D>();
        groundCheckMask = LayerMask.GetMask("Ground");
        defaultGravityScale = rigidbody2d.gravityScale;
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

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!groundCheck.IsTouchingLayers(groundCheckMask) && jumpsLeft <= 0) { return; }
        if (value.isPressed)
        {
            rigidbody2d.velocity += new Vector2(0, jumpHeight); 
            jumpsLeft--;
        }
    }

    void Run()
    {
        float moveX = moveInput.x * moveSpeed;
        rigidbody2d.velocity = new Vector2(Mathf.Clamp(moveX + rigidbody2d.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rigidbody2d.velocity.y, -maxSpeed, maxSpeed));
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
        if (transform.position.y < startGravityDecreaseAfter) { return; }
        float currentGravityScale;

        currentGravityScale = defaultGravityScale * Mathf.Pow(gravityScaleDecrease, transform.position.y - startGravityDecreaseAfter);
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

    internal void AddWateringCan(int value)
    {
        wateringCans += value;
        UpdateWateringCanUI();
    }

    private void UpdateWateringCanUI()
    {
        throw new NotImplementedException();
    }
}
