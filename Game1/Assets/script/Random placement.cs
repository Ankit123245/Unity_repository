using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign the prefab in the Inspector
    private Terrain terrain;
    public float spawnInterval = 120f; // 2 minutes
    public float objectLifetime = 60f; // 1 minute

    private void Start()
    {
        terrain = Terrain.activeTerrain; // Automatically find the terrain
        if (terrain == null)
        {
            Debug.LogError("No active terrain found in the scene!");
            return;
        }
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        if (objectToSpawn == null || terrain == null)
        {
            Debug.LogError("Missing required references!");
            return;
        }

        Vector3 spawnPosition = GetRandomPositionOnTerrain();
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        Destroy(spawnedObject, objectLifetime);
    }

    private Vector3 GetRandomPositionOnTerrain()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainX = terrain.transform.position.x;
        float terrainZ = terrain.transform.position.z;

        float randomX = Random.Range(terrainX, terrainX + terrainWidth);
        float randomZ = Random.Range(terrainZ, terrainZ + terrainLength);
       float terrainY = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + objectToSpawn.transform.localScale.y / 2;

        return new Vector3(randomX, terrainY, randomZ);
    }
}