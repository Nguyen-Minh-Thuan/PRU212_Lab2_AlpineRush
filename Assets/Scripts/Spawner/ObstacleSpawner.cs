using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


//public class ObstacleSpawner : MonoBehaviour
//{
//    [Header("Spawn Settings")]
//    public GameObject[] obstaclePrefabs;
//    public Transform player;
//    public float spawnInterval = 1.5f;
//    public float spawnDistanceAhead = 10f;

//    [Header("Lane Settings")]
//    public int numberOfLanes = 5;
//    public float laneWidth = 2f;
//    public float horizontalJitter = 0.3f;

//    private int lastZigzagLane = -1;

//    private List<GameObject> pool = new();
//    private int poolSize = 50;

//    void Start()
//    {
//        InitPool();
//        StartCoroutine(SpawnRoutine());
//    }

//    void InitPool()
//    {
//        for (int i = 0; i < poolSize; i++)
//        {
//            GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
//            GameObject obj = Instantiate(prefab);
//            obj.SetActive(false);
//            pool.Add(obj);
//        }
//    }

//    IEnumerator SpawnRoutine()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(spawnInterval);

//            int pattern = Random.Range(0, 3);

//            switch (pattern)
//            {
//                case 0: SpawnTripleRocks(); break;
//                case 1: SpawnZigzagTree(); break;
//                case 2: SpawnFenceWallWithGap(); break;
//            }
//        }
//    }

//    Vector3 GetLanePosition(int lane)
//    {
//        float baseX = player.position.x;
//        float laneOffset = (lane - numberOfLanes / 2) * laneWidth;
//        float yPos = player.position.y - spawnDistanceAhead;
//        return new Vector3(baseX + laneOffset + Random.Range(-horizontalJitter, horizontalJitter), yPos, 0f);
//    }

//    GameObject GetFromPool(GameObject prefab)
//    {
//        if (prefab == null)
//        {
//            Debug.LogError("Prefab is null. Ensure obstaclePrefabs are properly assigned.");
//            return null;
//        }

//        foreach (GameObject obj in pool)
//        {
//            if (!obj.activeInHierarchy && obj.name.Contains(prefab.name))
//                return obj;
//        }

//        // Optionally expand pool  
//        GameObject newObj = Instantiate(prefab);
//        newObj.SetActive(false);
//        pool.Add(newObj);
//        return newObj;
//    }

//    void SpawnObstacle(Vector3 position, GameObject prefab)
//    {
//        GameObject obj = GetFromPool(prefab);
//        if (obj != null)
//        {
//            obj.transform.position = position;
//            obj.SetActive(true);
//        }
//    }

//    GameObject GetObstacle(string type)
//    {
//        return obstaclePrefabs.FirstOrDefault(o => o.name.ToLower().Contains(type));
//    }

//    void SpawnTripleRocks()
//    {
//        List<int> lanes = new List<int>() { 0, 1, 2, 3, 4 };
//        lanes.Shuffle();

//        for (int i = 0; i < 3; i++)
//        {
//            SpawnObstacle(GetLanePosition(lanes[i]), GetObstacle("rock"));
//        }
//    }

//    void SpawnZigzagTree()
//    {
//        int newLane = lastZigzagLane == -1 ? Random.Range(0, numberOfLanes) :
//            Mathf.Clamp(lastZigzagLane + (Random.value > 0.5f ? 1 : -1), 0, numberOfLanes - 1);

//        lastZigzagLane = newLane;

//        SpawnObstacle(GetLanePosition(newLane), GetObstacle("tree"));
//    }

//    void SpawnFenceWallWithGap()
//    {
//        int gapLane = Random.Range(0, numberOfLanes);
//        for (int lane = 0; lane < numberOfLanes; lane++)
//        {
//            if (lane == gapLane) continue;
//            SpawnObstacle(GetLanePosition(lane), GetObstacle("fence"));
//        }
//    }

//}


public static class ShuffleExtension
{
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            int j = Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
