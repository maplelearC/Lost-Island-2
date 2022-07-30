using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool isEntered;//记录是否在可对话区域
    [TextArea(1, 3)]
    public string[] lines;
    [SerializeField] private bool hasName;//是否拥有名字
    private void OnTriggerEnter2D(Collider2D other)//检测是否进入到可对话区域
    {
        if (other.CompareTag("Player"))
        {
            isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)//检测是否离开可对话区域
    {
        if (other.CompareTag("Player"))
        {
            isEntered = false;
        }
    }

    private void Update()
    {
        if (isEntered && Input.GetKeyDown(KeyCode.Space) && DialogueManager.instance.dialogBox.activeInHierarchy == false)
        {
            DialogueManager.instance.ShowDialogue(lines, hasName);
        }
    }
}
