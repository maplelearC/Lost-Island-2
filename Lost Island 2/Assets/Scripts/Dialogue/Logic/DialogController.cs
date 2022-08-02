using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public DialogueData_SO dialogEmpty;
    public DialogueData_SO dialogFinish;

    private Stack<string> dialogEmptyStack;
    private Stack<string> dialogFinishStack;

    private bool isTalking;

    void Awake()
    {
        FillDialogStack();
    }

    private void FillDialogStack()
    {
        dialogEmptyStack = new Stack<string>();
        dialogFinishStack = new Stack<string>();

        for (int i = dialogEmpty.dialogueList.Count - 1; i > -1; i--)
        {
            dialogEmptyStack.Push(dialogEmpty.dialogueList[i]);
        }
        for (int i = dialogFinish.dialogueList.Count - 1; i > -1; i--)
        {
            dialogFinishStack.Push(dialogFinish.dialogueList[i]);
        }
    }

    public void ShowDialogueEmpty()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogEmptyStack));
        }
    }

    public void ShowDialogueFinish()
    {
        if (!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogFinishStack));
        }
    }

    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;
        if (data.TryPop(out string result))
        {
            EventHandler.CallShowDialogueEvent(result);
            yield return null;
            isTalking = false;
            EventHandler.CallGameStateChangedEvent(GameState.Pause);
        }
        else
        {
            EventHandler.CallShowDialogueEvent(String.Empty);
            FillDialogStack();
            isTalking = false;
            EventHandler.CallGameStateChangedEvent(GameState.GamePlay);
        }
    }
}
