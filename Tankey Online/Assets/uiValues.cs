using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class uiValues : MonoBehaviour
{
    public TMP_Text topText;

    public TMP_Text bottomText;

    public float value1 = 10;
    public float value2 = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (topText != null) topText.text = value1.ToString();
        if (bottomText != null) bottomText.text = value2.ToString();
    }

    void HitByRay(){
        value1 += value1;
        value2 -= value2;
    }
}
