using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GateGuardInteraction : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject Player;
    public Text dialogueText;
    public GameObject ButtCont;
    public string[] dialogue;
    private int index;
    private float wordSpeed = 0.05f;
    private bool playerInRange;

    //private string Message = "Welcome Traveller!. This is our beautiful castle society.";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialoguePanel.activeInHierarchy)
            {
                StopAllCoroutines();
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            UnityEngine.Cursor.visible = true;
            Screen.lockCursor = false;
            ButtCont.SetActive(true);
        }
    }

    public void NextLine()
    {
        ButtCont.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }
    }


    // This function main Usage is to reset the text of the dialogueText and the index.
    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }


    

    private IEnumerator Typing()
    {
        foreach (char Letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += Letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
                

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            StopAllCoroutines();
            ZeroText();
            UnityEngine.Cursor.visible = false;
            Screen.lockCursor = true;
        }
    }
}
