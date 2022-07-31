using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Animator animtransition;
    public void Respawn()
    {
        Debug.Log("cona");
        StartCoroutine(LoadLevel());
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameFunc());
    }

    IEnumerator LoadLevel()
    {
        
        //Play animation
        animtransition.SetTrigger("Close");
        //Wait for animation to finish
        yield return new WaitForSeconds(1.2f);
        //Load Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator QuitGameFunc()
    {
        //Play animation
        animtransition.SetTrigger("Close");
        //Wait for animation to finish
        yield return new WaitForSeconds(1.2f);
        //Load Scene
        SceneManager.LoadScene("MainMenu");

    }
}
