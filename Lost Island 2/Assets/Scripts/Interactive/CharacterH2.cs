using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogController))]
public class CharacterH2 : Interactive
{
    private DialogController dialogController;

    void Awake()
    {
        dialogController = GetComponent<DialogController>();
    }

    public override void EmptyClicked()
    {
        if (!isDone)
        {
            //对话内容A
            dialogController.ShowDialogueEmpty();
        }
        else
        {
            //对话内容B
            dialogController.ShowDialogueFinish();
        }
    }

    protected override void OnClickedAction()
    {
        //对话内容B
        dialogController.ShowDialogueFinish();
    }
}
