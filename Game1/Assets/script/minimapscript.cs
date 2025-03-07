using UnityEngine;
using UnityEngine.UI;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;  // Assign the Player's Transform in Inspector
    public RectTransform playerArrow; // Assign PlayerArrow (UI) in Inspector

    void LateUpdate()
    {
        // Follow player position (Only X and Z)
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y; // Keep MiniMap camera height fixed
        transform.position = newPosition;

        // Rotate the player arrow UI to match the player's rotation
        playerArrow.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);
    }
}
