using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float timer;
    float hidenTimer;

    [SerializeField] GameObject door;
    [SerializeField] Transform posToMoveTo;
    [SerializeField] float doorMovmentSpeedTo;
    [SerializeField] float doorMovmentSpeedBack;

    bool isHit;

    Vector2 doorStartPos;

    private void Start()
    {
        doorStartPos = door.transform.position;
    }

    private void Update()
    {
        if (isHit)
        {
            //door.SetActive(false);

            if (hidenTimer < 0)
            {
                //door.SetActive(true);
                isHit = false;
            }
            else
            {
                hidenTimer -= Time.deltaTime;
            }
            door.transform.position = Vector2.MoveTowards(
                    door.transform.position,
                    posToMoveTo.position,
                    doorMovmentSpeedTo * Time.deltaTime
                    );
        }
        else
        {
                door.transform.position = Vector2.MoveTowards(
                    door.transform.position, 
                    doorStartPos,
                    doorMovmentSpeedBack * Time.deltaTime
                    );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isHit) return; 
        hidenTimer = timer;
        isHit = true;

        print("Colided");
    }
}
