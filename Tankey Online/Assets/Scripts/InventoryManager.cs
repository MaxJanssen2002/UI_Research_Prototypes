using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated = false;
    private Keyboard keyboard = Keyboard.current;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(keyboard.eKey.isPressed && menuActivated){
            Time.timeScale = 1;
            Debug.Log("btn pressed menu turned off");
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }    
        else if(keyboard.eKey.isPressed && !menuActivated){
            Time.timeScale = 0;
            Debug.Log("btn pressed menu turned on");
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }
}
