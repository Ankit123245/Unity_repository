using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportTarget;  // Assign in Inspector
    public Terrain terrain;  // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object entered: " + other.name); // Debug log to check if detection works

        if (other.CompareTag("Player"))  // Only teleport the player
        {
            Debug.Log("Player detected! Teleporting...");

            Vector3 newPosition = teleportTarget.position;

            // Ensure the player lands on the terrain surface
            if (terrain != null)
            {
                float terrainY = terrain.SampleHeight(newPosition) + terrain.transform.position.y + 1f; // +1 to prevent sinking
                newPosition.y = terrainY;
            }

            other.transform.position = newPosition;  // Move the player instantly
        }
    }
}
