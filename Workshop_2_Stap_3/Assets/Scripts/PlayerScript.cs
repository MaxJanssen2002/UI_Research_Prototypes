using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    
    void Start()
    {
        movementSpeed = 2.0f;
        rotationSpeed = 120.0f;
    }
    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.forward * -movementSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(transform.TransformDirection(transform.up) * rotationSpeed * Time.deltaTime, Space.Self); 
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(transform.TransformDirection(transform.up) * -rotationSpeed * Time.deltaTime, Space.Self);  
    }
}