using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTarget : InteractablePuzzle
{
    [SerializeField] LayerMask canInteract;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            isActive = true;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            StartCoroutine(WaitRoutine());
    }

    IEnumerator WaitRoutine()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        isActive = false;
    }
}
