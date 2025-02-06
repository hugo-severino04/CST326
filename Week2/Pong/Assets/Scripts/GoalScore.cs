using UnityEngine;

public enum Player
{
    Left,
    Right
}
public class GoalScore : MonoBehaviour
{
    public bool isLeftGoal; // True for left goal, False for right goal

    // Reference to the GameOverview script
    public GameOverview gameOverview;

    private void OnTriggerEnter(Collider other)
    {
        // Ensure it's the ball entering the goal.
        if (other.CompareTag("Ball"))
        {
            if (gameOverview == null)
            {
                Debug.LogError("GameOverview reference is missing in GoalScore.");
                return;
            }

            // If the ball enters the left goal, the **RIGHT player scores**.
            // If the ball enters the right goal, the **LEFT player scores**.
            bool leftPlayerScored = !isLeftGoal; // Flip logic to ensure correctness

            gameOverview.PlayerScored(leftPlayerScored);
        }
    }
}