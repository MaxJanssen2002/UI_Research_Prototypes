using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRayCast : MonoBehaviour
{
    public float rayDistance = 100f; // Maximum ray distance
    private Keyboard keyboard = Keyboard.current;

    public playerInventory playerInventory;


    public Camera fpsCam;

    void Update()
    {
        Camera mainCam = fpsCam;
        if (mainCam == null) return;

        Vector3 origin = mainCam.transform.position;
        Vector3 direction = mainCam.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance))
        {
            hit.collider.gameObject.SendMessage("HitByRay", SendMessageOptions.DontRequireReceiver);
            if(keyboard.fKey.isPressed){
                GameObject PickUpObject = hit.collider.gameObject;
                if(PickUpObject.tag.Equals("PickUpAble")){
                    
                    Debug.Log("PickUp: " + hit.collider.name);
                    if(hit.collider.gameObject != null){
                        playerInventory.AddItem(PickUpObject);
                    }
                }
            }
        }
        Debug.DrawRay(origin, direction * rayDistance, Color.red);
    }
}
