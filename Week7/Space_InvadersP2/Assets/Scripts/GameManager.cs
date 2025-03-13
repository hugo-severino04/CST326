using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentScore = 0;
    public int highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDie += AddPoints;
        Player.OnPlayerDie += GameOver; 
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDie -= AddPoints;
        Player.OnPlayerDie -= GameOver; 
    }

    public void AddPoints(int points)
    {
        currentScore += points;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }

    public void RestartScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    public void GameOver()
    {
        Debug.Log("Player died! Loading Credits in 2 seconds...");
        StartCoroutine(GameOverSequence());
    }

    // that way there is some time between switching 
    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        SceneManager.LoadScene("Credits");
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score\n" + currentScore.ToString("D6");
        highScoreText.text = "HI-Score\n" + highScore.ToString("D6");
    }
}