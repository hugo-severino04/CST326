using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootingClip;
    public AudioClip deathClip;

    public Animator animator; 
    public delegate void EnemyDie(int points);
    public static event EnemyDie OnEnemyDie;

    // this is defining eney types we can then assign onto unity prefabs
    public enum EnemyType
    {
        Type1,
        Type2,
        Type3,
        Type4
    }
    
    public EnemyType enemyType;
    public GameObject enemyBulletPrefab;
    public Transform shottingOffset;
    private float fireRate = 25f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        StartCoroutine(FireRandomly());
    }

    private IEnumerator FireRandomly()
    {
        yield return new WaitForSeconds(Random.Range(0f, fireRate)); // Random initial delay

        while (true)
        {
            Fire();
            yield return
                new WaitForSeconds(Random.Range(fireRate * 1.0f, fireRate * 2.0f)); // Randomized shot intervals
        }
    }


    private void Fire()
    {
        animator.SetTrigger("shoot");
        audioSource.PlayOneShot(shootingClip);
        Instantiate(enemyBulletPrefab, shottingOffset.position, Quaternion.identity);
    }

    private int GetEnemyPoints()
    {
        switch (enemyType)
        {
            case EnemyType.Type1: return 10;
            case EnemyType.Type2: return 20;
            case EnemyType.Type3: return 30;
            case EnemyType.Type4: return 40;
            default: return 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bang! Enemy Hit");

            // Get points of enemy type from above
            int points = GetEnemyPoints();
            // Notify game manager 
            OnEnemyDie?.Invoke(points);
        
            // Play death sound
            if (deathClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathClip);
            }
            
            animator.SetTrigger("Die");

            Destroy(collision.gameObject);

            // Wait for the sound duration before destroying the enemy
            StartCoroutine(DestroyAfterSound());
            //StartCoroutine(DestroyAfterAnimation());
        }
    }
    private IEnumerator DestroyAfterSound()
    {
        // Wait for the length of the death sound before destroying the enemy
        yield return new WaitForSeconds(deathClip.length);
        Destroy(gameObject);
    }


    // private IEnumerator DestroyAfterAnimation()
    // {
    //     // Get the animation clip length
    //     float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
    //     // Wait for the longer of the two
    //     yield return new WaitForSeconds(Mathf.Max(animationLength, deathClip.length));
    // }
}