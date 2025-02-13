using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera cam;
    public Transform ballTransform;
    void Update()
    {
        cam.transform.LookAt(ballTransform.position);
    }
}
