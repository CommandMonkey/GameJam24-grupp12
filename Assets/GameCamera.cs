using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField, Range(0, 1)] float followWeight;

    Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) return;
        transform.position = (Vector2)Vector3.Lerp(transform.position, target.position, followWeight);
    }
}
