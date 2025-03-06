using UnityEngine;
using UnityEngine.AI; // Import NavMesh

public class EnemyFollowAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public float detectionRange = 15f; // Start following when within this range
    public float stoppingDistance = 2f; // Stop moving when this close to player

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get NavMeshAgent
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // Find player by tag

        if (playerObject != null)
        {
            player = playerObject.transform; // Set player transform
            agent.stoppingDistance = stoppingDistance; // Set stopping distance
        }
        else
        {
            Debug.LogError("Player not found! Make sure your Player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player == null) return; // Exit if no player found

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > stoppingDistance)
        {
            agent.SetDestination(player.position); // Move towards player
        }
        else
        {
            agent.ResetPath(); // Stop moving if too close or out of range
        }
    }
}
