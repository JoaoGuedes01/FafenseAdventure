using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public string sceneName;
    public Animator animtransition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadLevel(sceneName));
        }
    }

    IEnumerator LoadLevel(string sceneName)
    {
        //Play animation
        animtransition.SetTrigger("Close");
        //Wait for animation to finish
        yield return new WaitForSeconds(1.2f);
        //Load Scene
        SceneManager.LoadScene(sceneName);

    }
}
