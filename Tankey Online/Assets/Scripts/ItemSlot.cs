using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image itemImage;

    public Text itemNameText;
    public GameObject itemModel;

    public void SetItem(GameObject item)
    {
        
        // Sprite itemSprite = item.GetComponent<SpriteRenderer>()?.sprite;
        // if (itemSprite != null && itemImage != null)
        // {
        //     //itemImage.sprite = itemSprite;
        // }

        if (itemNameText != null)
        {
            itemNameText.text = item.name;
        }

        Debug.Log("Item set in slot: " + item.name);
    }
}
