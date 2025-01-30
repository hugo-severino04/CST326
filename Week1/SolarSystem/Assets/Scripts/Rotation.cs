using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float yawDegreesPerSecond = 45f; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = GetComponent<Transform>();
        myTransform.Rotate(0f, yawDegreesPerSecond * Time.deltaTime, 0f);
    }
}
