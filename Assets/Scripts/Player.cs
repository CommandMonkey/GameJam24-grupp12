using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] int amountOfJumps = 2;

    [Header("Shooting")]
    [SerializeField] GameObject StarBulletPrefab;

    Rigidbody2D myRigidbody;
    LayerMask groundCheckMask;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        //groundCheckMask = 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMove()
    {

    }
}
