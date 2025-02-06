using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;

    // Define how far from the spawn point the ball can appear on each axis.
    public float xRange = 2f;
    public float yRange = 0.5f;
    public float ballLifeTime = 5f;

    void Update()
    {
        // Using GetKeyDown to avoid spawning multiple balls per key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Generate random offsets for x-axis as Z-axis will always be 0
            // and y-axis range will be 0.5 
            float randomX = Random.Range(0f , xRange);
            // Add the offsets to the spawn point position.
            Vector3 spawnPos = ballSpawnPoint.position + new Vector3(randomX, yRange, 0);
            
            // new ball spawned 
            GameObject newObj = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
            Destroy(newObj, ballLifeTime);


        }
    }
}