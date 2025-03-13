using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shottingOffset;
    private Animator playerAnimator;

    public float playerSpeed = 4f;
    public float minX;
    public float maxX;
    public delegate void PlayerDie();
    public static event PlayerDie OnPlayerDie;
    
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip deathClip;
    
    private bool _isDead = false;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        // calculating screen boundarie using the camera
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        // adding small amount for player width
        minX = leftEdge.x + 0.5f;
        maxX = rightEdge.x - 0.5f;
        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetTrigger("Shoot Trigger");
            Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
            audioSource.PlayOneShot(audioClip);
            Debug.Log("Bang!");
        }
    }

    void PlayerMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float moveAmount = moveInput * playerSpeed * Time.deltaTime;

        // Move the player
        transform.position += new Vector3(moveAmount, 0, 0);

        // Clamp position to stay within screen
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("EnemyBullet") && !_isDead)
        {
            Debug.Log("Player hit!");
            _isDead = true;
            OnPlayerDie?.Invoke();
            
            audioSource.PlayOneShot(deathClip);

            // Trigger death animation
            playerAnimator.SetTrigger("Die");

            // Destroy the player after the animation finishes
            StartCoroutine(DestroyAfterAnimation());

            // Destroy the enemy bullet immediately
            Destroy(collision.gameObject);
        }
    }
    
    private IEnumerator DestroyAfterAnimation()    {
        // Get the length of the death animation
        float deathAnimationTime = playerAnimator.GetCurrentAnimatorStateInfo(0).length;

        // Wait for the animation to complete
        yield return new WaitForSeconds(deathAnimationTime);

        // Destroy the player GameObject
        Destroy(gameObject);
    }
}