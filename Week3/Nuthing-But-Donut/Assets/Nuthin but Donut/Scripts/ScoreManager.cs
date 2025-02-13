using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreManager : MonoBehaviour
{
    [Header("references")]
    public TextMeshProUGUI scoreText;
    public AudioClip crowdCheering;

    AudioSource audioSource;
    int score = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore()
    {
        score += 1;
        
        scoreText.text = $"Score: {score}";
        
        audioSource.clip = crowdCheering;
        audioSource.Play();
    }
}
