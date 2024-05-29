using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractablePuzzle
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        isActive = true;
        Destroy(collision.gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            StartCoroutine(WaitRoutine());
    }

    IEnumerator WaitRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        isActive = false;
    }
}
