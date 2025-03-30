using UnityEngine;

public class lookAtCam : MonoBehaviour
{
    public Camera fpsCam;
    void Update()
        {
            if (fpsCam != null)
            {
                Vector3 direction = fpsCam.transform.position - transform.position;
                direction.y = 0; // Lock Y-axis to keep UI upright
                transform.rotation = Quaternion.LookRotation(-direction);
            }
        }
}
