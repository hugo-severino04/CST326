using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Fire();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    private void Fire()
    {
        myRigidbody2D.linearVelocity = Vector2.down * speed;
        Debug.Log("Wwweeeeee Enemy");
    }
}