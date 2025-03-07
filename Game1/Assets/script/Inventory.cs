using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject[] inventorySlots = new GameObject[5]; // 5 inventory slots
    public Image[] slotImages; // UI Slot Images
    public Color selectedColor = Color.yellow;
    public Color defaultColor = Color.white;
    private int selectedSlot = -1;

    void Update()
    {
        // Select inventory slot using 1-5 keys
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                SelectSlot(i);
            }
        }
    }

    void SelectSlot(int slotIndex)
    {
        selectedSlot = slotIndex;
        Debug.Log($"Selected Slot {slotIndex + 1}: {inventorySlots[slotIndex]?.name ?? "Empty"}");

        // Update UI Colors
        for (int i = 0; i < slotImages.Length; i++)
        {
            slotImages[i].color = (i == selectedSlot) ? selectedColor : defaultColor;
        }
    }

    public bool AddItem(GameObject item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == null) // Find first empty slot
            {
                inventorySlots[i] = item;
                Debug.Log($"Picked up {item.name} in Slot {i + 1}");
                return true;
            }
        }
        Debug.Log("Inventory Full!");
        return false;
    }
}
