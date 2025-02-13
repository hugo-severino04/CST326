using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour
{
    public float initialSpeed = 10f;      // The starting speed for the ball.
    public float speedIncrement = 1f;     // How much the speed increases per paddle hit.
    public float maxBounce = 45f;    // Maximum bounce deflection (in degrees).
    public float resetDelay = 1f; 
    
    private float currentSpeed;
    private Rigidbody rb;
    private Vector3 originalPosition; // Stores the ball's initial position

    public AudioClip normalHitSound;
    public AudioClip edgeHitSound;
     
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        originalPosition = transform.position;
        
        // Set our current speed to the initial value.
        currentSpeed = initialSpeed;
        
        // Launch the ball initially along the y-axis.
        ServeBall(Random.value > 0.5f);    }

    // This method handles collisions with paddles.
    void OnCollisionEnter(Collision collision)
    {
        // Handle Paddle Collision
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // this hitfactor calculates where the ball hit 
            // on what side to determine if deflecting on right side or left side
            float hitFactor = (transform.position.z - collision.transform.position.z) / (collision.collider.bounds.size.z / 2f);
            hitFactor = Mathf.Clamp(hitFactor, -1f, 1f);
            float bounceAngle = hitFactor * maxBounce * Mathf.Deg2Rad;

            int verticalDirection;

            if (collision.gameObject.name.Contains("Left"))
            {
                // If the paddle's name contains "Left", set direction upward (1)
                verticalDirection = 1;
            }
            else
            {
                // If the paddle is not "Left" (it must be "Right"), set direction downward (-1)
                verticalDirection = -1;
            }
            currentSpeed += speedIncrement;

            float newVelocityY = verticalDirection * currentSpeed * Mathf.Cos(bounceAngle);
            float newVelocityZ = currentSpeed * Mathf.Sin(bounceAngle);

            rb.linearVelocity = new Vector3(0f, newVelocityY, newVelocityZ);
            
            AudioSource audioSrc = GetComponent<AudioSource>();
            // this means it hit new center 
            if (Mathf.Abs(hitFactor) < 0.1f) 
            {
                audioSrc.clip = normalHitSound;
                audioSrc.Play();
            }
            else
            {
                audioSrc.pitch = 1.8f;
                audioSrc.clip = edgeHitSound;
                audioSrc.Play();
            }
        }
        // Handle Wall Collision
        else if (collision.gameObject.CompareTag("Wall"))
        {
            float speedMultiplier = 1.1f;
            // Reverse ONLY the Z velocity while keeping the Y movement unchanged
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, -rb.linearVelocity.z);

            currentSpeed *= speedMultiplier;
            rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
            
            AudioSource audioSrc = GetComponent<AudioSource>();
            audioSrc.clip = normalHitSound;
            audioSrc.Play();
        }
    }
    
    public void ResetBall(bool scoredOnLeft)
    {
        // Stop movement and reset position
        rb.linearVelocity = Vector3.zero;
        transform.position = originalPosition;

        // Reset speed
        currentSpeed = initialSpeed;

        // Delay before serving
        Invoke(nameof(DelayedServe), resetDelay);
    }
    
    private void DelayedServe()
    {
        // Call ServeBall and determine serve direction based on last scored player
        ServeBall(Random.value > 0.5f);
    }

    private void ServeBall(bool scoredOnLeft)
    {
        int serveVerticalDirection;
        if (scoredOnLeft)
        {
            // If the left player was scored on, serve the ball downward (-1 on the y-axis).
            serveVerticalDirection = -1;
        }
        else
        {
            // If the right player was scored on, serve the ball upward (1 on the y-axis).
            serveVerticalDirection = 1;
        }
        
        rb.linearVelocity = new Vector3(0f, serveVerticalDirection * currentSpeed, 0f);
    }
}