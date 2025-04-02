using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeUI : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Button buyButton;
    public Button closeButton;
    private CustomerScript currentSeller;
    private PlayerScript player;
    private PlayerInventory playerInventory;
    private GameObject crossHair;

    private void Start(){
        if (playerInventory == null)
        {
            playerInventory = FindFirstObjectByType<PlayerInventory>();
            if (playerInventory == null)
            {
                Debug.LogError("PlayerInventory component not found!");
            }
        }
        crossHair = GameObject.Find("Invisible_Player/Canvas/Crosshair");
        closeButton.onClick.AddListener(() => DeactiveTradeUI());
        gameObject.SetActive(false);
    }
    
    public void OpenTrade(CustomerScript seller, PlayerScript playerScript)
    {
        currentSeller = seller;
        player = playerScript;
        UpdateShop();
        currentSeller.PlayIdleSound();
        // Show UI
        ActivateTradeUI();
    }

    void UpdateShop()
    {
        GameObject itemPrefab = currentSeller.GetItemForSale();
        int price = currentSeller.GetPrice();

        if (itemPrefab != null)
        {
            itemNameText.text = itemPrefab.name;
            priceText.text = $"Price: {price} Emeralds";

            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => TryBuyItem(itemPrefab, price));
            buyButton.interactable = true;
        }
        else
        {
            itemNameText.text = "No item available";
            priceText.text = "";
            buyButton.interactable = false;
        }
    }

    void TryBuyItem(GameObject itemPrefab, int price)
    {
        if (player.CanAfford(price))
        {
            player.SpendEmeralds(price); 
            playerInventory.AddItem( itemPrefab);
            Debug.Log($"Bought {itemPrefab.name} for {price} emeralds.");
            currentSeller.PlayAcceptSound();
        }
        else
        {
            Debug.Log("Not enough emeralds!");
        }
    }

    void ActivateTradeUI(){
        gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        crossHair.SetActive(false);
    }

    void DeactiveTradeUI(){
        gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
        crossHair.SetActive(true);
    }
}
