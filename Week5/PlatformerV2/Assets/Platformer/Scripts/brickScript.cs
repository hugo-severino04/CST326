using UnityEngine;

public class brickScript : MonoBehaviour
{
    public void breakingBrick()
    {
        Debug.Log("Brick Destroyed!!!!!");
        Destroy(gameObject);
    }
}
