using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowLeaves : MonoBehaviour
{
    [Tooltip("The target or button this object is coneected to")]
    [SerializeField] InteractablePuzzle ConnectedTo;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    private void Update()
    {
        if (ConnectedTo.CheckIfActiv)
            anim.speed = 1;
    }
}
