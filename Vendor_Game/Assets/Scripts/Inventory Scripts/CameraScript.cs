using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Transform player;


    void Start() {
        transform.position = player.position;
        transform.rotation = player.rotation;
    }


    void FixedUpdate() {
        transform.position = player.position;
        transform.rotation = player.rotation;
    }
}