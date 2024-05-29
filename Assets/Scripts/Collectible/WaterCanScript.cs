using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCanScript : Collectible
{
    bool WaterCan;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collect();
        WaterCan = true;
    }

    private void Start()
    {
        WaterCan = false;
    }
}
