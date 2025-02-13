using System.Collections;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float maxPaddleSpeed = 1f;
    public float paddleForce = 1f;
    public string inputAxis;
    public float minZ = -2.2f;
    public float maxZ = 2.2f;

    public float defaultPaddleSpeed;
    private Vector3 defaultPaddleSize;    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultPaddleSpeed = maxPaddleSpeed;
        defaultPaddleSize = transform.localScale;
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
    
    // speed boost 
    public IEnumerator ActivateSpeedBoost(float duration)
    {
        maxPaddleSpeed *= 1.5f; // Increase speed by 50%
        yield return new WaitForSeconds(duration);
        maxPaddleSpeed = defaultPaddleSpeed;
    }
    // changing paddle size 
    public IEnumerator ChangePaddleSize(float sizeMultiplier, float duration)
    {
        transform.localScale = new Vector3(defaultPaddleSize.x, defaultPaddleSize.y, defaultPaddleSize.z * sizeMultiplier);
        yield return new WaitForSeconds(duration);
        transform.localScale = defaultPaddleSize; // Reset to original size
    }
}
