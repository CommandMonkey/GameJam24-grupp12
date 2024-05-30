using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position =
           FindObjectOfType<WateringShrineCollectable>().transform.position;
    }

}
