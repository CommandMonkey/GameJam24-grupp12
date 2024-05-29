using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] int jumpAmount = 2;
    [SerializeField] float startGravityDecreaseAfter = 10f;
    [SerializeField, Range(0f, 1f)] float gravityScaleDecrease = 1f;
    [SerializeField, Min(0.1f)] float minGravity = 0.1f;

    [Header("Shooting")]
    [SerializeField] GameObject StarBulletPrefab;
    [SerializeField] float bulletSpeed;

    Vector2 moveInput;
    int jumpsLeft;

    Vector2 mousePos;
    float defaultGravityScale;


    Rigidbody2D myRigidbody;
    Collider2D groundCheck;
    LayerMask groundCheckMask;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<Collider2D>();
        groundCheckMask = LayerMask.GetMask("Ground");
        defaultGravityScale = myRigidbody.gravityScale;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ResetJumpCheck();
        CalculateGravityScale();
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
        if (!groundCheck.IsTouchingLayers(groundCheckMask) && jumpsLeft - 1 <= 0) { return; }
        if (value.isPressed)
        {
            myRigidbody.velocity = new Vector2(0, jumpHeight);
            jumpsLeft--;
        }
    }

    void Run()
    {
        myRigidbody.velocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
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
        Debug.Log(mousePos);
        Quaternion StarBulletRotation = Quaternion.FromToRotation(transform.position, mousePos);
        GameObject starInstance = Instantiate(StarBulletPrefab, transform.position, StarBulletRotation);

        starInstance.GetComponent<Rigidbody2D>().velocity = starInstance.transform.right * bulletSpeed;
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
        myRigidbody.gravityScale = currentGravityScale;
    }
}
