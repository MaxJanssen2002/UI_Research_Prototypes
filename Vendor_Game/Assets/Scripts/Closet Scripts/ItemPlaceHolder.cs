using System;
using UnityEngine;

public class ItemPlaceHolder : MonoBehaviour
{
    public GameObject heldItem;
    private GameObject visualItem; 

    private GameObject _closet;
    private Closet _closetScript;

    private void Start()
    {
        _closet = transform.parent.gameObject;
        _closetScript = _closet.GetComponent<Closet>();
    }
    
    public void PlaceItem(GameObject item)
    {
        if (item != null)
        {
            Vector3 spawnPosition = transform.position;
            spawnPosition.y = transform.position.y - 0.2f;

            // Adjust the y position based on the item type if needed
            if (item.name.Contains("Master_Ball"))
            {
                spawnPosition.y = transform.position.y - 0.1f;
            }

            if (item.name.Contains("Book"))
            {
                spawnPosition.y = transform.position.y - 0.18f;
            }

            GameObject itemInstance = Instantiate(item, spawnPosition, transform.rotation);
            itemInstance.SetActive(true);
            itemInstance.transform.SetParent(transform);

            heldItem = item;
            visualItem = itemInstance;
            _closetScript.AddToCount();
        }
    }
    
    public void RemoveItem()
    {
        if (heldItem != null)
        {
            Destroy(visualItem);
            heldItem = null;
            _closetScript.RemoveFromCount();
        }
    }
}