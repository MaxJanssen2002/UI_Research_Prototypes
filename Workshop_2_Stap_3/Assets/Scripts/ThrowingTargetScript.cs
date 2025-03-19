using UnityEngine;

public class ThrowingTargetScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "ThrownObject") {
            Debug.Log("Collision with object");
        }
    }
}
