using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public GameObject startRoad;
    public GameObject finish;

    public Transform snakeTransform;
    
    private float roadZPos = 0;
    private int startRoadsCount = 3;
    private float roadLength;
    private int roadIndex;
    private List<GameObject> currentRoads = new List<GameObject>();

    private float safeZone = 130f;
    private bool isGameActive;

    void Start()
    {
        isGameActive = true;
        roadIndex = Random.Range(0, roadPrefabs.Length - 1);
        roadZPos = startRoad.transform.position.z;
        roadLength = startRoad.GetComponent<BoxCollider>().bounds.size.z;

        for (int i = 0; i < startRoadsCount; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            CheckSpawn();
        }
    }

    private void SpawnRoad()
    {
        GameObject road = Instantiate(roadPrefabs[roadIndex]);

        roadZPos += roadLength;
        road.transform.position = new Vector3(0, 0, roadZPos);

        roadIndex = (roadIndex + Random.Range(1, roadPrefabs.Length - 1)) % roadPrefabs.Length;

        currentRoads.Add(road);
    }

    private void SpawnFinish()
    {
        GameObject road = Instantiate(finish);
        roadZPos += roadLength;
        road.transform.position = new Vector3(0, 0, roadZPos);
    }

    private void CheckSpawn()
    {
        if (snakeTransform.position.z - safeZone > (roadZPos - startRoadsCount * roadLength))
        {
            SpawnRoad();
            DestroyRoad();
        }
    }

    private void DestroyRoad()
    {
        Destroy(currentRoads[0]);
        currentRoads.RemoveAt(0);
    }

    public void GameFinish()
    {
        isGameActive = false;
        SpawnFinish();
    }
}
