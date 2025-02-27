using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Mario's Transform
    public Vector3 offset = new Vector3(3f, 3f, -10f); // Adjust for best positioning
    public float leftLimit = 0f; // Prevents camera from moving left

    private float fixedY; // Stores the camera's fixed Y position

    void Start()
    {
        fixedY = transform.position.y; // Save the initial Y position
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, fixedY, offset.z);

            // Prevent the camera from moving left
            if (desiredPosition.x < leftLimit)
            {
                desiredPosition.x = leftLimit;
            }

            // Instantly move the camera to the new position (no Lerp)
            transform.position = desiredPosition;
        }
    }
}