using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource _audioSource;

    void Awake()
    {
        instance = this;
        // making sure doesnt get destroyed
        DontDestroyOnLoad(this);
        // getting the audio comp
        _audioSource = GetComponent<AudioSource>();
    }
}
