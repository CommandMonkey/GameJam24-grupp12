using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] int jumpAmount = 2;

    Vector2 moveInput;
    Vector2 jumpInput;
    [SerializeField] int jumpsLeft;

    [Header("Shooting")]
    [SerializeField] GameObject StarBulletPrefab;

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
        if (groundCheck.IsTouchingLayers(groundCheckMask))
        {
            jumpsLeft = jumpAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove()
    {

    void OnJump(InputValue value)
    {
        if (!groundCheck && jumpsLeft <= 0) { return; }
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
}
