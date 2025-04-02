using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated = false;
    private Keyboard keyboard = Keyboard.current;
    private GameObject crossHair;

    void Start()
    {
        InventoryMenu.SetActive(false);
        crossHair = transform.parent.Find("Canvas/Crosshair")?.gameObject;
    }

    void Update()
    {
        if(keyboard.eKey.wasReleasedThisFrame && menuActivated){
            Time.timeScale = 1;
            Debug.Log("btn pressed menu turned off");
            InventoryMenu.SetActive(false);
            menuActivated = false;
            Cursor.lockState = CursorLockMode.Locked;  
            Cursor.visible = false;
            crossHair.SetActive(true);

        }    
        else if(keyboard.eKey.wasPressedThisFrame && !menuActivated){
            Time.timeScale = 0;
            Debug.Log("btn released menu turned on");
            InventoryMenu.SetActive(true);
            menuActivated = true;          
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
            crossHair.SetActive(false);
        }
    }
}
