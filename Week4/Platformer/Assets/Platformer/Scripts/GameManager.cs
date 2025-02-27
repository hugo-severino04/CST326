using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI coinText;
    private float timeLeft = 300f;
    private int points = 0;
    private int coins = 0;

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            // making sure no negative time
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
        }
        UpdateUI();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Brick"))
                {
                    hit.collider.GetComponent<brickScript>()?.breakingBrick();
                }
                else if (hit.collider.CompareTag("QuestionBlock"))
                {
                    hit.collider.GetComponent<QuestionMarkScript>()?.hitQuestionBlock();
                }
            }
        }
    }

    public void UpdateUI()
    {
        // updating time
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
        timeText.text = $"Time:\n{(int)timeSpan.TotalSeconds}";

        // updating coins 
        coinText.text = "x " + coins.ToString("D2");

        // updating points 
        pointText.text = "Mario \n" + points.ToString("D6");
    }
    // method to add coins
    public void addCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }
    // method to add points 
    public void addPoints(int amount)
    {
        points += amount;
        UpdateUI();
    }
}
