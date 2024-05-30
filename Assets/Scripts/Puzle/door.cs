using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool stayOpen;
    
    [SerializeField] float howLongTostayOpen;
    float timeUntilClosed;
    [SerializeField] float doorOpeningSpeed;
    [SerializeField] float doorClosingSpeed;

    [SerializeField] Transform posToMoveTo;

    [Tooltip("The target or button this door is coneected to")]
    [SerializeField] InteractablePuzzle ConnectedTo;

    Vector2 startPos;
    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        if(stayOpen)
            timeUntilClosed = howLongTostayOpen;

        if (ConnectedTo.CheckIfActiv)
            OpenDoor();
        else
            CloseDoor();
    }

    private void CloseDoor()
    {
        if(timeUntilClosed <= 0) 
        MoveDoor(startPos, doorClosingSpeed);
        else 
        timeUntilClosed -= Time.deltaTime;
    }

    void OpenDoor()
    {
        timeUntilClosed = howLongTostayOpen;
        MoveDoor(posToMoveTo.position, doorOpeningSpeed);
    }
    void MoveDoor(Vector2 moveTo, float Speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, moveTo, Speed * Time.deltaTime);
    }
}
