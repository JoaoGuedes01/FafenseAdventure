using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool isPlayerOverLapping=false;
    public PlayerControlAlternative player;
    public Animator PressEPrompt;
    
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        FindObjectOfType<DialogueManager>().popUp();
        
    }

    private void Update()
    {
        if (isPlayerOverLapping==true && Input.GetKeyDown(KeyCode.E) && player.hasPressedE==false)
        {
            TriggerDialogue();
            player.PressedETrue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PressEPrompt.SetTrigger("Open");
            isPlayerOverLapping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PressEPrompt.SetTrigger("Close");
            isPlayerOverLapping = false;
        }
    }
}
