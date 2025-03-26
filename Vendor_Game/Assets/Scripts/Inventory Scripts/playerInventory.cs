using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public Transform inventorySlots;
    public GameObject itemSlotPrefab;

    public int maxInventorySize = 20;

    public void AddItem(GameObject item)
    {
        if (items.Count <= maxInventorySize)
        {
            Debug.Log("itemAdded");
            items.Add(item);
            item.SetActive(false);
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
            GameObject uiSlot = Instantiate(itemSlotPrefab, inventorySlots);
            ItemSlot slotComponent = uiSlot.GetComponent<ItemSlot>();
            if (slotComponent != null)
            {
                slotComponent.SetItem(item);
            }
            else
            {
                Debug.LogError("ItemSlot component not found on the itemSlotPrefab!");
            }
        }
    }
}
