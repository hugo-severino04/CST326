using UnityEngine;

public class QuestionMarkAnimation : MonoBehaviour
{
    public float animationSpeed = 0.2f;
    private Renderer rend;
    private float [] offsets = {0f, 0.2f, 0.4f, 0.6f, 0.8f, 1f};
    private int currentOffset = 0;
    private float timer = 0f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.mainTextureScale = new Vector2(-1f, -0.2f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentOffset = (currentOffset + 1) % offsets.Length; // Cycle through offsets

            // Update only the Y offset
            Vector2 newOffset = new Vector2(0f, offsets[currentOffset]); 
            rend.material.mainTextureOffset = newOffset;
        }
    }
}
