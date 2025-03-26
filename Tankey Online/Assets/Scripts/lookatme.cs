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
        DisableUI();
    }

    void HitByRay () {
        Canvas objectUI = GetComponentInChildren<Canvas>();

        if (objectUI != null){
            objectUI.gameObject.SetActive(true);
        }
        else
        objectUI = GetComponentInChildren<Canvas>(true);
        objectUI.gameObject.SetActive(true);
    }

    void DisableUI(){
            Canvas objectUI = GetComponentInChildren<Canvas>();

        if (objectUI != null){
            objectUI.gameObject.SetActive(false);
        }
        else
        objectUI = GetComponentInChildren<Canvas>(true);
        objectUI.gameObject.SetActive(false);
    }
}
