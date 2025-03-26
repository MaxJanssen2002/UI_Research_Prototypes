using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryDataScriptableObject", menuName = "Scriptable Objects/InventoryDataScriptableObject")]
public class InventoryDataScriptableObject : ScriptableObject
{
    public List<GameObject> items = new List<GameObject>();
    public int maxInventorySize = 20;

    public void AddItem(GameObject item)
    {
        if (items.Count < maxInventorySize)
        {
            Debug.Log("Item Added: " + item.name);
            items.Add(item);
            item.SetActive(false); // Hide the item in the world
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void RemoveItem(GameObject item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Item Removed: " + item.name);
        }
        else
        {
            Debug.LogWarning("Tried to remove an item that doesn't exist in inventory.");
        }
    }

}
