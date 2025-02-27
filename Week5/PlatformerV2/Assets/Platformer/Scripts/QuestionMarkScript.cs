using UnityEngine;

public class QuestionMarkScript : MonoBehaviour
{
    public int coinValue = 1;
    public int pointsPerCoin = 100;
    
    public void hitQuestionBlock()
    {
        Debug.Log("QuestionMark hit! Plus 1 coin and +100 points");

        // Find the first GameManager instance and add coins + points
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.addCoins(coinValue); // Adds 1 coin
            gameManager.addPoints(pointsPerCoin); // Adds 100 points
        }
    }
}
