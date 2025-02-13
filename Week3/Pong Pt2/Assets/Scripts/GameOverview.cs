using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverview : MonoBehaviour
{
    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;
    public Ball ball;
    
    public TMP_Text scoreText;
    public Color normalColor = Color.white;
    public Color leftLead = Color.green;
    public Color rightLead = Color.red;
    public float scoreEffectTime = 0.5f;
    
    public void PlayerScored(bool leftScored)
    {
        if (leftScored)
        {
            leftPlayerScore++;
            Debug.Log("Left Player scores! Score is now: " + leftPlayerScore + " - " + rightPlayerScore);
        }
        else
        {
            rightPlayerScore++;
            Debug.Log("Right Player scores! Score is now: " + leftPlayerScore + " - " + rightPlayerScore);
        }
        
        UpdateScoreText();
        
        // Check for game over (first to 11 points)
        if (leftPlayerScore >= 11)
        {
            Debug.Log("Game Over, Left Paddle Wins!");
            ResetGame();
            return; // Prevent further execution
        }
        else if (rightPlayerScore >= 11)
        {
            Debug.Log("Game Over, Right Paddle Wins!");
            ResetGame();
            return; // Prevent further execution
        }

        // Reset the ball after scoring
        if (ball != null)
        {
            if (leftScored)
            {
                ball.ResetBall(false); // Serve toward the right player
            }
            else
            {
                ball.ResetBall(true); // Serve toward the left player
            }        
        }
        StartCoroutine(ScoreTextEffect());
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score " + "\n" + "Player one: " + leftPlayerScore + 
                         " \n" + "Player Two: " + rightPlayerScore;
        if (leftPlayerScore > rightPlayerScore)
        {
            scoreText.color = leftLead;
        } else if (leftPlayerScore < rightPlayerScore)
        {
            scoreText.color = rightLead;
        }
        else
        {
            scoreText.color = normalColor;
        }
    }
    private IEnumerator ScoreTextEffect()
    {
        float originalSize = scoreText.fontSize;
        float enlargedSize = originalSize * 1.5f; // Increase size temporarily

        scoreText.fontSize = enlargedSize;

        yield return new WaitForSeconds(scoreEffectTime); // Wait for effect to last

        scoreText.fontSize = originalSize; // Reset to original size
    }
    private void ResetGame()
    {
        // Reset scores and log
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        Debug.Log("Scores have been reset to 0-0.");

        // Reset ball to start position
        if (ball != null)
        {
            ball.ResetBall(Random.value > 0.5f); // Randomly serve the ball
        }
    }
}
