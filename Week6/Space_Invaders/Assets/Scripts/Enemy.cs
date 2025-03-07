using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
    private float fireRate = 10f;

    private void Start()
    {
        StartCoroutine(FireRandomly());
    }

    private IEnumerator FireRandomly()
    {
        yield return new WaitForSeconds(Random.Range(0f, fireRate)); // Random initial delay

        while (true)
        {
            Fire();
            yield return
                new WaitForSeconds(Random.Range(fireRate * 0.5f, fireRate * 1.5f)); // Randomized shot intervals
        }
    }


    private void Fire()
    {
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

            // get points of enemy type from above
            int points = GetEnemyPoints();
            // notify game manager 
            OnEnemyDie?.Invoke(points);

            Destroy(collision.gameObject);
            //will kill enemy
            Destroy(gameObject);
        }
    }
}