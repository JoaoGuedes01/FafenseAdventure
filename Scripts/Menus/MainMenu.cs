using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject OptionsButton;
    public string Level2Code;
    public string Level3Code;
    public string Level4Code;
    public string Level5Code;


    public TMP_InputField inputField;
    private Animator animStartButton;
    private Animator animOptButton;
    public Animator animCongrats;
    public Animator animWarAlr;
    public Animator animWarInv;
    public Animator animWarnLckd;
    public Animator animtransition;





    //Levels and Level Sprites
    public Sprite Level2Sprite;
    public Image Level2;

    public Sprite Level3Sprite;
    public Image Level3;
    public void Start()
    {
        animStartButton = StartButton.GetComponent<Animator>();
        animOptButton = OptionsButton.GetComponent<Animator>();
    }

     public void StartButtonPressed()
    {
        animStartButton.SetTrigger("Disabled");
        animOptButton.SetTrigger("Pressed");
        
    }

    public void OptionsButtonPressed()
    {
        animStartButton.SetTrigger("Pressed");
    }

    public void LoadLevel1()
    {
        StartCoroutine(LoadLevel("tutorial"));
    }

    public void LoadLevel2()
    {
        if (Level2Code == "codigonivel2")
        {
            StartCoroutine(LoadLevel("Village"));
        }
        else
        {
            animWarnLckd.SetTrigger("Open");
            Debug.Log("nao tens o codigo do nivel 2");
        }
    }

    public void LoadLevel3()
    {
        if (Level3Code == "lCuTof")
        {
            StartCoroutine(LoadLevel("Level1"));
        }
        else
        {
            animWarnLckd.SetTrigger("Open");
            Debug.Log("nao tens o codigo do nivel 1");
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

    

    public void LoadCode()
    {
        
        if (inputField.text == "codigonivel2" && Level2Code != "codigonivel2")
        {
            Level2.GetComponent<Image>().sprite = Level2Sprite;
            Level2Code = inputField.text;
            animCongrats.SetTrigger("Open");
        }
        else if (inputField.text == "codigonivel2" && Level2Code == "codigonivel2")
        {
            Debug.Log("Level 2 already unlocked");
            animWarAlr.SetTrigger("Open");
            
        }
        if (inputField.text == "lCuTof" && Level3Code != "lCuTof")
        {
            Level3.GetComponent<Image>().sprite = Level3Sprite;
            Level3Code = inputField.text;
            animCongrats.SetTrigger("Open");
        }
        else if (inputField.text == "lCuTof" && Level3Code == "lCuTof")
        {
            Debug.Log("Level 3 already unlocked");
            animWarAlr.SetTrigger("Open");
        }
        if (inputField.text == "Level4Code" && Level4Code != "Level4Code")
        {
            Level4Code = inputField.text;
        }
        else if(inputField.text == "Level4Code" && Level4Code == "Level4Code")
        {
            Debug.Log("Level 4 already unlocked");
            animWarAlr.SetTrigger("Open");
        }
        if (inputField.text == "Level5Code" && Level5Code != "Level5Code")
        {
            Level5Code = inputField.text;
        }
        else if(inputField.text == "Level5Code" && Level5Code == "Level5Code")
        {
            Debug.Log("Level 5 already unlocked");
            animWarAlr.SetTrigger("Open");
        }
        else if(inputField.text != "codigonivel2" && inputField.text != "lCuTof" && inputField.text != "Level4Code" && inputField.text != "Level25Code")
        {
            Debug.Log("Code Not Valid");
            animWarInv.SetTrigger("Open");
        }


    }
}
