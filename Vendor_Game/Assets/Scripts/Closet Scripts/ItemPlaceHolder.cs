using System;
using UnityEngine;

public class ItemPlaceHolder : MonoBehaviour
{
    public GameObject item;
    public GameObject heldItem;
    
    private GameObject _closet;
    private Closet _closetScript;
    private GameObject _player;
    private PlayerScript _playerScript;
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerScript = _player.GetComponent<PlayerScript>();
        _closet = transform.parent.gameObject;
        _closetScript = _closet.GetComponent<Closet>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Interacted");

        if (heldItem != null)
        {
            RemoveItem();
        }
        else
        {
            PlaceItem(_playerScript.Inventory[0]);
        }
    }

    private void PlaceItem(GameObject item)
    {
        if (item != null)
        {
            heldItem = Instantiate(item, transform.position, transform.rotation);
            _closetScript.AddToCount();
        }
    }

    private void RemoveItem()
    {
            Destroy(heldItem);
            heldItem = null;
            _closetScript.RemoveFromCount();
    }
}