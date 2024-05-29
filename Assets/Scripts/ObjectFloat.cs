using System.Collections;
using UnityEngine;

public class SmoothUpDownMovement : MonoBehaviour
{
    public float moveAmount = 1/1.8f; 
    public float moveDuration = 0.7f; 

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveUpAndDown());
    }

    IEnumerator MoveUpAndDown()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(originalPosition + new Vector3(0, moveAmount, 0), moveDuration));
            yield return StartCoroutine(MoveToPosition(originalPosition, moveDuration));
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the final position is set correctly
        transform.position = targetPosition;
    }
}

