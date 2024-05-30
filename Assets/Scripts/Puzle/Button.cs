using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractablePuzzle
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        isActive = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
            StartCoroutine(WaitRoutine());
    }

    IEnumerator WaitRoutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        isActive = false;
    }
}
