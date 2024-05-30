using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    // Spawn pos
    private Vector3 leftSpawnPosition = new Vector3(-152, 0, 0);
    private Vector3 rightSpawnPosition = new Vector3(152, 0, 0);

    private float minY = 50f;
    private float maxY = 250f;

    private float minZ = 0f;
    private float maxZ = 30f;

    public float cloudSpeed = 10f;
    public float spawnInterval = 5f;

    void Start()
    {
        // invoke cloud spawning spawn
        InvokeRepeating("SpawnCloud", 0f, spawnInterval);
    }

    void SpawnCloud()
    {
        // random prefab
        int randomIndex = Random.Range(0, cloudPrefabs.Length);
        GameObject cloudPrefab = cloudPrefabs[randomIndex];

        // random y pos
        float randomY = Random.Range(minY, maxY);
        // randm z pos
        float randomZ = Random.Range(minZ, maxZ);

        // left or right
        bool spawnOnLeft = Random.value > 0.5f;
        Vector3 spawnPosition = spawnOnLeft ? leftSpawnPosition : rightSpawnPosition;
        spawnPosition.y = randomY;
        spawnPosition.z = randomZ;

        GameObject cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);

        // Add the CloudMover
        CloudMover mover = cloud.AddComponent<CloudMover>();
        mover.Initialize(spawnOnLeft, -cloudSpeed);
    }
}

public class CloudMover : MonoBehaviour
{
    private bool movingLeft;
    private float speed;

    // Da cloud mova
    public void Initialize(bool movingLeft, float speed)
    {
        this.movingLeft = movingLeft;
        this.speed = speed;
    }
    void Update()
    {
        // Move cloud
        if (movingLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // Destroy cloud if it off screen
        if (transform.position.x < -200 || transform.position.x > 200)
        {
            Destroy(gameObject);
        }
    }
}
