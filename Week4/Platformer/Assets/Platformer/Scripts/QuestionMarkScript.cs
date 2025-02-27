using UnityEngine;

public class QuestionMarkScript : MonoBehaviour
{
    public int coinValue = 1;
    
    public void hitQuestionBlock()
    {
        Debug.Log("QuestionMark hit! Plus 1 coin");

        // Find the first GameManager instance and add coins
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.addCoins(coinValue);
        }
        else
        {
            Debug.LogWarning("GameManager not found in scene!");
        }
    }
}
