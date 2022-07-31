using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CarrocaLevelLoader : MonoBehaviour
{
    public string sceneName;
    public Animator animtransition;
    public Animator animCharacter;
    public Animator animLevelComplete;
    public Animator animPressEnter;
    public Animator animCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadLevel(sceneName));
        }
    }

    IEnumerator LoadLevel(string sceneName)
    {

        //Trigger Camera Animation
        animCamera.SetTrigger("LevelComplete");
        yield return new WaitForSeconds(0.2f);
        //Trigger Level Complete Prompt
        animLevelComplete.SetTrigger("LevelComplete");
        animPressEnter.SetTrigger("LevelComplete");
        //Trigger Celebartion Animation
        animCharacter.SetBool("IsLevelFinished", true);
        //Wait for enter input
        yield return waitForKeyPress(KeyCode.Return);
        animLevelComplete.SetTrigger("PressEnter");
        animPressEnter.SetTrigger("PressEnter");
        //Play animation
        animtransition.SetTrigger("Close");
        //Wait for animation to finish
        yield return new WaitForSeconds(1.2f);
        //Load Scene
        SceneManager.LoadScene(sceneName);

    }

    private IEnumerator waitForKeyPress(KeyCode key)
    {
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(key))
            {
                done = true; // breaks the loop
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

        // now this function returns
    }
}
