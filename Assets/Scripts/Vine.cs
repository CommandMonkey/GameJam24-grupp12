using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    [SerializeField] bool grow;
    public bool Grow { get { return grow; } set { grow = value; } }

    private void Update()
    {
        if (grow)
        {
            GrowVine();
        }
    }
    void GrowVine()
    {

    }
}