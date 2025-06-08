using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Assign in Inspector to put in the spawn pool

	// How far left/right obstacles can spawn
	public float spawnInterval = 1.5f;
    public float spawnDistanceAhead = 10f;
    public float spawnRangeX = 5f; 

    public Transform player; // Assign player transform to track

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        // Randomly select an obstacle prefab
        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Random X position within range
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);

        // Spawn ahead of the player (downhill, so negative Y)
        Vector2 spawnPos = new(spawnX, player.position.y - spawnDistanceAhead);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
