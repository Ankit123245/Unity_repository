using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject[] inventorySlots = new GameObject[5]; // Inventory slots
    private int selectedSlot = -1; // No slot selected initially

    void Update()
    {
        // Select slot with number keys (1-5)
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                selectedSlot = i;
                Debug.Log($"Selected Slot {i + 1}: {inventorySlots[i]?.name ?? "Empty"}");
            }
        }
    }

    public bool AddItem(GameObject item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null) // Find empty slot
            {
                inventorySlots[i] = item;
                item.SetActive(false); // Hide the item from the world
                Debug.Log($"Picked up {item.name} in Slot {i + 1}");
                return true;
            }
        }
        Debug.Log("Inventory Full!");
        return false;
    }
}
