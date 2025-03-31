using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerInventory : MonoBehaviour
{
    public InventoryDataScriptableObject inventoryData;
    public Transform inventorySlots;
    public GameObject itemSlotPrefab;

    void Start()
    {
        inventoryData = AssetDatabase.LoadAssetAtPath<InventoryDataScriptableObject>("Assets/InventoryData.asset");
        if (!inventoryData) {
            inventoryData = ScriptableObject.CreateInstance<InventoryDataScriptableObject>();
            AssetDatabase.CreateAsset(inventoryData, "Assets/InventoryData.asset");
        }
        RefreshUI();
    }

    public void AddItem(GameObject item)
    {
        inventoryData.AddItem(item);
        RefreshUI();
    }

    public void RemoveItem(GameObject item)
    {
        inventoryData.RemoveItem(item);
        RefreshUI();
    }

    void RefreshUI()
    {
        foreach (Transform child in inventorySlots)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject item in inventoryData.items)
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
    }}
