using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoButton : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void LoadGameScene()
    {
        StartCoroutine(_LoadGameScene());

        IEnumerator _LoadGameScene()
        {
            AsyncOperation loadOp = SceneManager.LoadSceneAsync("SpaceInvaders");
            while (!loadOp!.isDone) yield return null;
            
            GameObject player = GameObject.Find("Player");
            Debug.Log(player.name);
        }
    }
}
