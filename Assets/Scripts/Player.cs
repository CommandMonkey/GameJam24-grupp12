using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] int jumpAmount = 2;

    [Header("Shooting")]
    [SerializeField] GameObject StarBulletPrefab;

    Vector2 moveInput;
    int jumpsLeft;


    Rigidbody2D myRigidbody;
    Collider2D groundCheck;
    LayerMask groundCheckMask;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<Collider2D>();
        groundCheckMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        ResetJumpCheck();
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
}
