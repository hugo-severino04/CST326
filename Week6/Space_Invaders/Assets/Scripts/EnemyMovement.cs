using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;

    public float moveDown = 0.5f;

    // 1 is right and -1 is left 
    private int direction = 1;
    public float pauseTime = 0.5f;
    private float moveTimer = 0f;
    private int totalEnemies;
    public float speedIncrease = 5f;
    private bool hasMovedDown = false;

    private void Start()
    {
        totalEnemies = transform.childCount;
    }

    private void Update()
    {
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime; // Reduce timer
        }
        else
        {
            MoveEnemies(); // Move enemies when the timer reaches zero
            moveTimer = pauseTime; // Reset timer for the next move
        }
    }

    private void MoveEnemies()
    {
        IncreaseSpeed();
        transform.position += Vector3.right * (speed * direction * Time.deltaTime);
        if (EdgeOfScreen() && !hasMovedDown)
        {
            direction *= -1;
            transform.position += Vector3.down * moveDown;
            hasMovedDown = true;
        }
        else if (!EdgeOfScreen())
        {
            hasMovedDown = false;
        }
    }

    private void IncreaseSpeed()
    {
        int currentRemainingEnemies = transform.childCount;
        int enemiesKilled = totalEnemies - currentRemainingEnemies;
        if (enemiesKilled > 0)
        {
            speed = 5f * Mathf.Pow(1.8f, enemiesKilled * speedIncrease);
        }
    }

    private bool EdgeOfScreen()
    {
        foreach (Transform child in transform)
        {
            if (child)
            {
                float screenX = Camera.main.WorldToViewportPoint(child.position).x;
                if (screenX <= 0.05f || screenX >= 0.95f) // Check left or right screen bounds
                    return true;
            }
        }

        return false;
    }
}