
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TerrainTools;

public class Cannon : MonoBehaviour
{
    public Vector2 moveInput;

    public Material material1;

    public Material material2;

    public Transform barrelTip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Keyboard keyboard = Keyboard.current;
    private static GameObject lastHitObject = null; 
    void Update()
    { 
        splashCannonUpdate();
        uIUpdate();
        
    }

    void splashCannonUpdate(){
        if(keyboard.spaceKey.isPressed){
                Debug.DrawRay(barrelTip.position, transform.up, Color.black);    
                if( Physics.Raycast(barrelTip.position, transform.up, out RaycastHit hitInfo, 20f))
                {
                    Debug.Log("Something");
                    if(hitInfo.collider.tag.Equals("Paintable")){
                        Debug.Log("Paintable");
                        Renderer colliderRender = hitInfo.collider.GetComponent<Renderer>();
                        if (colliderRender != null){
                            Debug.Log(colliderRender.material.color.ToString());
                            Debug.Log(material1.color.ToString());
                            if(colliderRender.material.color == material1.color)
                            {
                                colliderRender.material = material2;
                            }
                            else
                            {
                                colliderRender.material = material1;
                            }
                        }
                    }
                }
                else{
                    Debug.Log("Nothing");
                }
                
            }
    }

    void uIUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(barrelTip.position, transform.up, out hit, 20f))
        {
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);
            if(lastHitObject != null){
            lastHitObject.SendMessage("DisableUI", SendMessageOptions.DontRequireReceiver);
            }
            hit.collider.gameObject.SendMessage("HitByRay", SendMessageOptions.DontRequireReceiver);
            lastHitObject = hit.collider.gameObject;
        }
        else
        {
            if(lastHitObject != null){
            lastHitObject.SendMessage("DisableUI", SendMessageOptions.DontRequireReceiver);
            Debug.Log("Raycast did not hit anything.");
            }
        }
    }

}
