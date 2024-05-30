using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] GameObject deathScrean;

    GameObject player;

    private void Start()
    {
        deathScrean.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Dead");

        deathScrean.SetActive(true);
        player = FindObjectOfType<Player>().gameObject;
    }

    public void Respawn()
    {
        player.transform.position = 
            FindObjectOfType<WateringShrineCollevctable>().transform.position;

        deathScrean.SetActive(false);
    }
}
