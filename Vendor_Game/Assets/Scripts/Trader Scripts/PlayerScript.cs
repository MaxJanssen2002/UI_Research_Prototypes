using UnityEngine;
using UnityEditor;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private WorldInfo worldInfo;

    public int emeraldCount;
    [SerializeField] private TMP_Text emeraldCountText;
    private float targetCustomerDistance;
    private float rayDistance;
    private float maxRayDistance;
    private Ray shootingRay;
    private RaycastHit hit;

    private bool selectingItem;
    private ItemPlaceHolder itemPlaceHolder;
    
    [SerializeField]
    private GameObject inventorySlots;
    [SerializeField] 
    private GameObject InventoryMenu;
    public TradeUI tradeUI;
    [SerializeField]
    private PlayerInventory playerInventory;
    private RectTransform inventorySlotsRect;
    [SerializeField]
    private TMP_Text selectItemPrompt;
    [SerializeField]
    private mouselook cameraTurner;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        rb = GetComponent<Rigidbody>();
        LoadWorldInfo();
        targetCustomerDistance = worldInfo.targetCustomerDistance;
        maxRayDistance = 100.0f;
        rayDistance = maxRayDistance;
        emeraldCount = 10;
        itemPlaceHolder = null;
        selectingItem = false;

        if (inventorySlots) { inventorySlotsRect = inventorySlots.GetComponent<RectTransform>(); }
        if (selectItemPrompt) { selectItemPrompt.gameObject.SetActive(false); }
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            InteractWithCustomer();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!selectingItem) {
                InteractWithCloset();
            }
            else {
                SelectItemToPlace();
            }
        }
    }


    private void Update()
    {
        if (emeraldCountText) {
            emeraldCountText.text = emeraldCount.ToString();
        }
    }


    private void LoadWorldInfo()
    {
        worldInfo = AssetDatabase.LoadAssetAtPath<WorldInfo>("Assets/WorldInfo.asset");
        if (!worldInfo)
        {
            worldInfo = ScriptableObject.CreateInstance<WorldInfo>();
            AssetDatabase.CreateAsset(worldInfo, "Assets/WorldInfo.asset");
        }
    }


    private void InteractWithCustomer()
    {
        shootingRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(shootingRay.origin, transform.TransformDirection(Vector3.forward), out hit, 25f))
        {
            rayDistance = hit.distance;

            if (hit.transform.gameObject.tag == "Customer")
            {

                CustomerScript customer = hit.transform.GetComponent<CustomerScript>();

                if (customer != null && customer.role == CustomerScript.Role.Seller &&
                    Vector3.Distance(transform.position, hit.transform.position) <= targetCustomerDistance)
                {
                    Debug.Log($"Opening trade with seller offering {customer.GetItemForSale().name} for {customer.GetPrice()} emeralds.");
                    if (tradeUI) {
                        tradeUI.OpenTrade(customer, this); // Pass the specific seller to the UI
                    }
                }
            }
        }
        else
        {
            rayDistance = maxRayDistance;
        }
    }

    private void InteractWithCloset()
    {
        // Use the camera's forward direction for the raycast
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found. Ensure the camera is tagged as 'MainCamera'.");
            return;
        }

        shootingRay = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (Physics.Raycast(shootingRay.origin, shootingRay.direction, out hit, rayDistance))
        {
            Debug.Log("Raycast hit");
            rayDistance = hit.distance;

            if (hit.transform.gameObject.CompareTag("ItemPlaceHolder"))
            {
                itemPlaceHolder = hit.transform.GetComponent<ItemPlaceHolder>();

                if (itemPlaceHolder != null && Vector3.Distance(transform.position, hit.transform.position) <= targetCustomerDistance)
                {
                    if (itemPlaceHolder.heldItem != null)
                    {
                        GameObject itemToRemove = itemPlaceHolder.heldItem;
                        itemPlaceHolder.RemoveItem();
                        playerInventory.AddItem(itemToRemove);
                        Debug.Log("Added " + itemToRemove.name + " to inventory");
                        Debug.Log("Inventory count: " + playerInventory.inventoryData.items.Count);
                    }
                    else if (playerInventory.inventoryData.items.Count > 0)
                    {
                        InventoryMenu.SetActive(true);
                        if (selectItemPrompt) { selectItemPrompt.gameObject.SetActive(true); }
                        cameraTurner.shouldRotate = false;
                        selectingItem = true;
                        Cursor.visible = true; 
                        Cursor.lockState = CursorLockMode.None;
                    }
                    else
                    {
                        Debug.Log("No items in inventory to place.");
                    }
                }
            }
        }
        else
        {
            rayDistance = maxRayDistance;
        }
    }

    public bool CanAfford(int price)
    {
        return emeraldCount >= price;
    }

    public void SpendEmeralds(int amount)
    {
        emeraldCount -= amount;
    }

    private void SelectItemToPlace() {

        int itemIndex = CheckInventorySlots();

        if (itemPlaceHolder && itemIndex != -1) {
            GameObject itemToPlace = playerInventory.inventoryData.items[itemIndex];
            itemPlaceHolder.PlaceItem(itemToPlace);
            playerInventory.RemoveItem(itemToPlace);
            Debug.Log("Removed " + itemToPlace.name + " from inventory");

            InventoryMenu.SetActive(false);
            if (selectItemPrompt) { selectItemPrompt.gameObject.SetActive(false); }
            cameraTurner.shouldRotate = true;
            selectingItem = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    private int CheckInventorySlots() {
        int amountOfChildren = inventorySlots.transform.childCount;
        float posX, posY, width, height;
        
        for (int childIndex = 0; childIndex < amountOfChildren; ++childIndex) {
            RectTransform rect = inventorySlots.transform.GetChild(childIndex).GetComponent<RectTransform>();

            if (rect) {
                posX = rect.anchoredPosition.x;
                posY = -rect.anchoredPosition.y;
                width = rect.sizeDelta.x;
                height = rect.sizeDelta.y;

                if (InRectangle( MousePosInInventory(), posX, posY, width, height )) {
                    return childIndex;
                }
            }
        }
        return -1;
    }


    private Vector2 MousePosInInventory() {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(inventorySlotsRect, Input.mousePosition, null, out mousePos);
        mousePos.x += (inventorySlotsRect.rect.width / 2);
        mousePos.y = (inventorySlotsRect.rect.height / 2) - mousePos.y;
        return mousePos;
    }


    private bool InRectangle(Vector2 pos, float rectX, float rectY, float rectWidth, float rectHeight) {
        return pos.x >= rectX - rectWidth / 2
            && pos.x <= rectX + rectWidth / 2
            && pos.y >= rectY - rectHeight / 2
            && pos.y <= rectY + rectHeight / 2;
    }
}