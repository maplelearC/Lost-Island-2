using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeUI : MonoBehaviour
{
    public GameObject panel;
    public Text dialogText;

    void OnEnable()
    {
        EventHandler.ShowDialogueEvent += ShowDialogue;
    }

    void OnDisable()
    {
        EventHandler.ShowDialogueEvent += ShowDialogue;
    }

    
    private void ShowDialogue(string dialogue)
    {
        if (dialogue != string.Empty)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
        dialogText.text = dialogue;
    }

   
}
