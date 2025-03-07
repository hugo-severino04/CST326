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

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        // calculating screen boundarie using the camera
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        // adding small amount for player width
        minX = leftEdge.x + 0.5f;
        maxX = rightEdge.x - 0.5f;
    }

    void Update()
    {
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnimator.SetTrigger("Shoot Trigger");
            Instantiate(bulletPrefab, shottingOffset.position, Quaternion.identity);
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
        if (collision.gameObject.CompareTag("EnemyBullet2"))
        {
            Debug.Log("Player hit! ");
            OnPlayerDie?.Invoke();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}