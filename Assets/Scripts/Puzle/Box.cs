using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] float interactableRange;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] bool follow;

    [SerializeField]Transform playerTra;
    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, interactableRange,playerLayer);
        if (Input.GetKeyDown(KeyCode.E) && player != null && !follow)
        {
            follow = true;
            playerTra = player.transform;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.E) && follow)
        {
            follow = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        if (follow)
        {
            MoveBox();
        }
    }

    private void MoveBox()
    {
        float dist = Vector2.Distance(transform.position, playerTra.position);
        if (dist > 1.6) 
        {
            Vector2 target = new Vector2(playerTra.position.x, playerTra.position.y+1);
            transform.position = Vector2.MoveTowards(transform.position, target, 5* Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactableRange);
    }
}
