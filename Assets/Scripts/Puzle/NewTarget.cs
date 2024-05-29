using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTarget : InteractablePuzzle
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isActive = true;
    }
}
