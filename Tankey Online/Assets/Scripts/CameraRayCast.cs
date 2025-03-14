using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    public float rayDistance = 100f; // Maximum ray distance

    public Camera fpsCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the camera transform
        Camera mainCam = fpsCam;
        if (mainCam == null) return;

        // Define ray origin and direction
        Vector3 origin = mainCam.transform.position;
        Vector3 direction = mainCam.transform.forward;

        // Perform the raycast
        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
            
            hit.collider.gameObject.SendMessage("HitByRay", SendMessageOptions.DontRequireReceiver);

        }

        // Debugging: Draw the ray in the Scene View
        Debug.DrawRay(origin, direction * rayDistance, Color.red);
    }
}
