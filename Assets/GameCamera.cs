using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField, Range(0, 1)] float followWeight;

    Camera camera;

    float startZ;


    // Start is called before the first frame update
    void Start()
    {
        startZ = transform.position.z;
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        Vector3 newPos = Vector3.Lerp(transform.position, target.position, followWeight);
        newPos.z = startZ;
        newPos.y = Mathf.Max(0, newPos.y);
        transform.position = newPos;
    }
}
