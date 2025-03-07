using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class RandomCapsuleMovement : MonoBehaviour
{
    public Terrain terrain;
    public float waitTime = 2f; // Time before choosing a new position
    public float moveRadius = 10f; // Adjust how far it moves each time
    public GameObject prefabToAttach; // Prefab to attach to the capsule

    private NavMeshAgent agent;
    private Vector3 lastPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        if (terrain == null)
        {
            terrain = Terrain.activeTerrain;
            if (terrain == null)
            {
                Debug.LogError("No active terrain found in the scene!");
                return;
            }
        }
        
        AttachPrefab();
        lastPosition = transform.position;
        StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            ChooseNewTargetPosition();
            yield return new WaitForSeconds(waitTime);
        }
    }

    void ChooseNewTargetPosition()
    {
        Vector3 randomPoint;
        NavMeshHit hit;
        
        do
        {
            randomPoint = GetRandomPointOnTerrain();
        } while (Vector3.Dot((randomPoint - lastPosition).normalized, transform.forward) < 0); // Avoid backward movement

        if (NavMesh.SamplePosition(randomPoint, out hit, moveRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
            lastPosition = transform.position;
        }
    }

    Vector3 GetRandomPointOnTerrain()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        float terrainX = terrain.transform.position.x;
        float terrainZ = terrain.transform.position.z;

        float randomX = Random.Range(terrainX, terrainX + terrainWidth);
        float randomZ = Random.Range(terrainZ, terrainZ + terrainLength);
        float terrainY = terrain.SampleHeight(new Vector3(randomX, 0, randomZ));

        return new Vector3(randomX, terrainY, randomZ);
    }

    void AttachPrefab()
    {
        if (prefabToAttach != null)
        {
            GameObject attachedObject = Instantiate(prefabToAttach, transform);
            attachedObject.transform.localPosition = new Vector3(0, 1, 0); // Adjust position if needed
        }
    }
}
