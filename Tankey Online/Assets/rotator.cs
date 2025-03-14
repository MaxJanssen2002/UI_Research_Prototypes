using UnityEngine;

public class rotator : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0f, 50f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);

    }
}
