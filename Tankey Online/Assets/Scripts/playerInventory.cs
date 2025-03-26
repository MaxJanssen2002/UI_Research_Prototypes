using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>(); // Store picked objects
    public Transform inventorySlots;
    public GameObject itemSlotPrefab; // Prefab for UI slot

    public int maxInventorySize = 20;

    public void AddItem(GameObject item)
    {
        if (items.Count <= maxInventorySize) // Check for space
        {
            Debug.Log("itemAdded");
            items.Add(item);
            item.SetActive(false); // Hide object in world
            DisplayItemInUI(item);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    void DisplayItemInUI(GameObject item)
    {
        if (items.Count <= maxInventorySize)
        {
            // Add a new slot if the inventory is not full
            GameObject uiSlot = Instantiate(itemSlotPrefab, inventorySlots);
            ItemSlot slotComponent = uiSlot.GetComponent<ItemSlot>();
            if (slotComponent != null)
            {
                slotComponent.SetItem(item); // Assign item to the new slot
            }
            else
            {
                Debug.LogError("ItemSlot component not found on the itemSlotPrefab!");
            }
        }
    }
}
