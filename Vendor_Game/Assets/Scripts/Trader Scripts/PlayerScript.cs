using UnityEngine;
using UnityEditor;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private WorldInfo worldInfo;

    [SerializeField]
    public TMP_Text emeraldCountText;
    private float targetCustomerDistance;
    private float rayDistance;
    private float maxRayDistance;
    private Ray shootingRay;
    private RaycastHit hit;
    public int emeraldCount;
    public TradeUI tradeUI;
    
    public PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        rb = GetComponent<Rigidbody>();
        LoadWorldInfo();
        targetCustomerDistance = worldInfo.targetCustomerDistance;
        maxRayDistance = 100.0f;
        rayDistance = maxRayDistance;
        emeraldCount = 10;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            InteractWithCustomer();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            InteractWithCloset();
        }
    }


    private void Update()
    {
        emeraldCountText.text = emeraldCount.ToString();
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
                    tradeUI.OpenTrade(customer, this); // Pass the specific seller to the UI
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
                ItemPlaceHolder itemPlaceHolder = hit.transform.GetComponent<ItemPlaceHolder>();

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
                        GameObject itemToPlace = playerInventory.inventoryData.items[0];
                        itemPlaceHolder.PlaceItem(itemToPlace);
                        playerInventory.RemoveItem(itemToPlace);
                        Debug.Log("Removed " + itemToPlace.name + " from inventory");
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
}