using UnityEngine;

public class lookatme : MonoBehaviour
{
    void Start()
    {
        DisableUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void HitByRay () {
        Debug.Log ("I was hit by a Ray");
        Canvas objectUI = GetComponentInChildren<Canvas>();

        if (objectUI != null){
            objectUI.gameObject.SetActive(true);
        }
        else
        objectUI = GetComponentInChildren<Canvas>(true);
        objectUI.gameObject.SetActive(true);
    }

    void DisableUI(){
        Debug.Log ("Disable UI");
        Canvas objectUI = GetComponentInChildren<Canvas>();

        if (objectUI != null){
            objectUI.gameObject.SetActive(false);
        }
        else
        objectUI = GetComponentInChildren<Canvas>(true);
        objectUI.gameObject.SetActive(false);
    }
}
