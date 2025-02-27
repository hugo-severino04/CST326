using UnityEngine;

public class brickScript : MonoBehaviour
{
    public void breakingBrick()
    {
        Debug.Log("Breaking brick function called! +100 Points");

        // Give the player 100 points
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.addPoints(100);
        }
        // Destroy the brick
        Destroy(gameObject); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player collided with the brick!");

            // Check ALL contact points
            foreach (ContactPoint contact in collision.contacts)
            {
                Vector3 hitDirection = contact.normal;
                // If contact comes from below, break the brick
                if (hitDirection.y > 0.5f) 
                {
                    breakingBrick();
                    return; 
                }
            }
        }
    }
}
