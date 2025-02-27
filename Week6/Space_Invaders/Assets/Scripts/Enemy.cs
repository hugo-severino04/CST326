using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDie(int points);

    public static event EnemyDie OnEnemyDie;
    
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
      Debug.Log("Ouch!");
      Destroy(collision.gameObject);
      
      OnEnemyDie?.Invoke(3);
      
      //todo kill enemy 
    }
}
