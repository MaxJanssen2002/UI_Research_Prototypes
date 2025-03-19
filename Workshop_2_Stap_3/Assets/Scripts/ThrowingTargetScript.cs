using UnityEngine;
using UnityEngine.UI;

public class ThrowingTargetScript : MonoBehaviour
{
    [SerializeField]
    private Text score;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "ThrownObject") {
            score.text = "Yay, you hit it!";
        }
    }
}
