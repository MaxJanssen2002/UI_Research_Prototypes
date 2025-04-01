using UnityEngine;
using UnityEditor;
using TMPro;
using System.Collections.Generic;



public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private WorldInfo worldInfo;

    [SerializeField]
    public TMP_Text emeraldCountText;

    private float movementSpeed;
    private float rotationSpeed;

    private float targetCustomerDistance;
    private float rayDistance;
    private float maxRayDistance;
    private Ray shootingRay;
    private RaycastHit hit;
    public int emeraldCount;
    public TradeUI tradeUI;

    public List<GameObject> Inventory = new List<GameObject>();
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        LoadWorldInfo();
        targetCustomerDistance = worldInfo.targetCustomerDistance;
        movementSpeed = 2.0f;
        rotationSpeed = 120.0f;
        maxRayDistance = 100.0f;
        rayDistance = maxRayDistance;
        emeraldCount = 10;
    }
    
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            rb.MovePosition(transform.position + -transform.forward * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -rotationSpeed, 0) * Time.deltaTime);
            rb.MoveRotation(transform.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
            rb.MoveRotation(transform.rotation * deltaRotation);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            InteractWithCustomer();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            InteractWithCloset();
        }
    }


    private void Update() {
        emeraldCountText.text = emeraldCount.ToString();
    }


    private void LoadWorldInfo() {
        worldInfo = AssetDatabase.LoadAssetAtPath<WorldInfo>("Assets/WorldInfo.asset");
        if (!worldInfo) {
            worldInfo = ScriptableObject.CreateInstance<WorldInfo>();
            AssetDatabase.CreateAsset(worldInfo, "Assets/WorldInfo.asset");
        }
    }


    private void InteractWithCustomer() {
        shootingRay = new Ray (transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(shootingRay.origin, transform.TransformDirection(Vector3.forward), out hit, 25f)) {
            rayDistance = hit.distance;
            
            if (hit.transform.gameObject.tag == "Customer") {

                CustomerScript customer = hit.transform.GetComponent<CustomerScript>();

                if (customer != null && customer.role == CustomerScript.Role.Seller && 
                    Vector3.Distance(transform.position, hit.transform.position) <= targetCustomerDistance)
                {
                    Debug.Log($"Opening trade with seller offering {customer.GetItemForSale().name} for {customer.GetPrice()} emeralds.");
                    tradeUI.OpenTrade(customer,this); // Pass the specific seller to the UI
                }
            }
        }
        else {
            rayDistance = maxRayDistance;
        }
    }

    private void InteractWithCloset()
    {
        shootingRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(shootingRay.origin, transform.TransformDirection(Vector3.forward), out hit, rayDistance))
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
                        Inventory.Add(itemToRemove);
                        Debug.Log("Added " + itemToRemove.name + " to inventory");
                        Debug.Log("Inventory count: " + Inventory.Count);
                    }
                    else
                    {
                        GameObject itemToPlace = Inventory[0];
                        itemPlaceHolder.PlaceItem(itemToPlace);
                        Inventory.RemoveAt(0);
                        Debug.Log("Removed " + itemToPlace.name + " from inventory");
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