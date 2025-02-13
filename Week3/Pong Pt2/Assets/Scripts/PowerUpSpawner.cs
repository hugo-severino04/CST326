using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Array of different power-up prefabs
    public Transform[] spawnPoints; // Holds the 4 predetermined spawn points
    public float spawnInterval = 10f; // Time between spawns

    void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (spawnPoints.Length > 0 && powerUpPrefabs.Length > 0)
            {
                // Select a random spawn point from the 4 available
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                
                // Select a random power-up from the available prefabs
                GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

                // Spawn the power-up at the chosen spawn point
                Instantiate(powerUpPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
}