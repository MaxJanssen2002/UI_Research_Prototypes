using UnityEngine;
using UnityEngine.InputSystem;

public class headMovement : MonoBehaviour
{
    Keyboard keyboard = Keyboard.current;

    public float turnSpeed = 50f;

    private bool nKeyPressed;
    private bool mKeyPressed;

    void Update()
    {
        nKeyPressed = keyboard.nKey.isPressed;
        mKeyPressed = keyboard.mKey.isPressed;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(nKeyPressed){
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * -1);
        }
        if(mKeyPressed){
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
    }

    
}
