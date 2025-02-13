using UnityEngine;

public enum PowerUpType
{
    SpeedBoost,
    EnlargePaddle
}

public class PowerUps : MonoBehaviour
{
    public PowerUpType powerUpType;
    // How long the power-up lasts 5 seconds
    public float duration = 5f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            PaddleController paddle = other.GetComponent<PaddleController>();

            if (paddle != null)
            {
                ApplyPowerUp(paddle);
                Destroy(gameObject); // Remove power-up after activation
            }
        }
    }
    private void ApplyPowerUp(PaddleController paddle)
    {
        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost:
                StartCoroutine(paddle.ActivateSpeedBoost(duration));
                break;

            case PowerUpType.EnlargePaddle:
                StartCoroutine(paddle.ChangePaddleSize(2.5f, duration)); // Enlarges paddle
                break;
        }
    }
}