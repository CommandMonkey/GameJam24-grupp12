using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Collectible
{

    public GameObject Blockage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Blockage != null) 
        { 
         Collect();
         Blockage.SetActive(false);
        }
    }

    void RemoveBlockage()
    {
        if (Collect())
        {
            
        }
    }
}
