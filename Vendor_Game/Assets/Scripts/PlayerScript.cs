using UnityEngine;
using UnityEditor;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private WorldInfo worldInfo;
    public float movementSpeed;
    public float rotationSpeed;

    private float targetCustomerDistance;
    private float rayDistance;
    private float maxRayDistance;
    private Ray shootingRay;
    private RaycastHit hit;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        LoadWorldInfo();
        targetCustomerDistance = worldInfo.targetCustomerDistance;
        movementSpeed = 2.0f;
        rotationSpeed = 120.0f;
        maxRayDistance = 100.0f;
        rayDistance = maxRayDistance;
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
        if (Input.GetKeyDown(KeyCode.E)) {
            InteractWithCustomer();
        }
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
        if (Physics.Raycast(shootingRay.origin, transform.TransformDirection(Vector3.forward), out hit, rayDistance)) {
            rayDistance = hit.distance;
            if (hit.transform.gameObject.tag == "Customer") {
                if (Vector3.Distance(transform.position, hit.transform.position) <= targetCustomerDistance) {
                    Debug.Log("Interacting with customer");
                }
            }
        }
        else {
            rayDistance = maxRayDistance;
        }
    }
}