using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject objectToThrow;
    public float movementSpeed;
    public float rotationSpeed;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed = 2.0f;
        rotationSpeed = 120.0f;
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
            throwObject();
        }
    }


    private void throwObject() {
        GameObject newObject = Instantiate(objectToThrow, transform.position + transform.forward * 0.5f, Quaternion.identity);
        newObject.name = "Sphere";
    }
}