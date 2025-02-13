using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI timeText;

    private void Update()
    {
        int timeLeft = 300 - (int)(Time.time);
    }
}
