using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float maxPaddleSpeed = 1f;
    public float paddleForce = 1f;
    public string inputAxis;
    public float minZ = -2.2f;
    public float maxZ = 2.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoxCollider c = GetComponent<BoxCollider>();
        float max = c.bounds.max.z;
        float min = c.bounds.min.z; 
        //Debug.Log($"max: {max}, min: {min}");
    }

    // Update is called once per frame
    void Update()
    {
        float movementAxis = Input.GetAxis(inputAxis);
        
        Transform paddleTransform = GetComponent<Transform>();
        Vector3 newPosition = paddleTransform.position + new Vector3(0f, 0f, movementAxis * maxPaddleSpeed * Time.deltaTime);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        
        paddleTransform.position = newPosition;
        
    }
}
