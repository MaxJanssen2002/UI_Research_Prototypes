using System;
using UnityEngine;

public class ItemPlaceHolder : MonoBehaviour
{
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
        if (heldItem != null)
        {
            _playerScript.Inventory.Add(heldItem);
            RemoveItem();
        }
        else
        {
            for (int i = 0; i < _playerScript.Inventory.Count; i++)
            {
                if (_playerScript.Inventory[i] != null)
                {
                    PlaceItem(_playerScript.Inventory[i]);
                    _playerScript.Inventory.RemoveAt(i);
                    break;
                }
            }
        }
    }

    private void PlaceItem(GameObject item)
    {
        if (item != null)
        {
            GameObject newItem = Instantiate(item, transform.position, transform.rotation);
            newItem.SetActive(true);
            heldItem = newItem;
            _closetScript.AddToCount();
        }
    }

    private void RemoveItem()
    {
        if (heldItem != null)
        {
            heldItem.SetActive(false);
            heldItem = null;
            _closetScript.RemoveFromCount();
        }
    }
}