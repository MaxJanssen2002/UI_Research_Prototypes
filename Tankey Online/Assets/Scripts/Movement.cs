using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 100f;
    public Vector2 moveInput;
    
    public float hoverHeight;

    public LayerMask layerMask;
    void FixedUpdate ()
    {
        // if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        //     transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        
        // if(Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S))
        //     transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        
        // if(Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
        //     transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        
        // if(Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
        //     transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * moveInput.y);
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * moveInput.x);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, hoverHeight * 2, layerMask))
        {
            //Debug.Log(hit.distance);
            Debug.DrawRay(transform.position, Vector3.down);
            float targetY = hit.point.y + hoverHeight;
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, targetY, Time.deltaTime * moveSpeed), transform.position.z);

            Quaternion groundRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, groundRotation, Time.deltaTime * moveSpeed);
        } 
    }


    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
}
