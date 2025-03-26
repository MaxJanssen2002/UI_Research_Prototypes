using UnityEngine;

public class orientationController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Camera fpsCam;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraEuler = fpsCam.transform.eulerAngles; // Get camera rotation
        transform.rotation = Quaternion.Euler(0, cameraEuler.y, 0); // Apply only Y rotation
    }
}
