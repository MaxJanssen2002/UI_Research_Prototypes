using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Closet : MonoBehaviour
{
    private int _count = 0; 
    public TMP_Text countText;
    private List<GameObject> _itemPlaceHolders = new List<GameObject>();
    
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag("ItemPlaceHolder"))
            {
                _itemPlaceHolders.Add(child);
            }
        }
        
        countText.text = "Items: " + _count;
    }
    
    public void AddToCount()
    {
        _count++;
        countText.text = "Items: " + _count;
    }
    
    public void RemoveFromCount()
    {
        _count--;
        countText.text = "Items: " + _count;
    }
}
