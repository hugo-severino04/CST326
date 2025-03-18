using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditManager : MonoBehaviour
{
    private void Start()
    {
        // start the countdown now 
        StartCoroutine(ReturnMainMenu());
    }

    private IEnumerator ReturnMainMenu()
    {
        // wait 5 seconds 
        yield return new WaitForSeconds(6f); 
        // load the menu scene back
        SceneManager.LoadScene("MainMenu"); 
    }
}
