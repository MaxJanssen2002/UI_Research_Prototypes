using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class mouselook : MonoBehaviour
{

    public float sens = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
    
    xRotation -= mouseY;
    xRotation = math.clamp(xRotation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX); 
    }
}
