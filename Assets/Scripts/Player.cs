using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] Transform playerGraphics;

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
    [SerializeField] int jumpsLeft;

    Vector2 mousePos;
    float defaultGravityScale;

    Cursor cursor;

    Rigidbody2D rigidbody2d;
    Collider2D groundCheck;
    LayerMask groundCheckMask;

    // Start is called before the first frame update
    void Start()
    {
        cursor = FindObjectOfType<Cursor>();

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
            rigidbody2d.velocity = new Vector2(0, jumpHeight);
            jumpsLeft--;
        }
    }

    void Run()
    {
        rigidbody2d.velocity = new Vector2(moveInput.x * moveSpeed, rigidbody2d.velocity.y);
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
}
