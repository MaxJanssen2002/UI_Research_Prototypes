using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image itemImage;
    public GameObject itemModel;

    public void SetItem(GameObject item)
    {

        if (item == null)
        {
            Debug.LogError("SetItem called with a null item!");
            return;
        }

        Debug.Log("Item set in slot: " + item.name);

        ItemData itemData = item.GetComponent<ItemData>();
        if (itemData != null && itemData.itemSprite != null)
        {
            itemImage.sprite = itemData.itemSprite;
        }
        else
        {
            Debug.LogWarning("No sprite found for item: " + item.name);
        }
    }
}
