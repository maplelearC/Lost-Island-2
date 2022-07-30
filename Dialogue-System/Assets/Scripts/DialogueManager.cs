using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialogBox;//对话框
    public Text dialogText, nameText;//对话文字、姓名文字
    [TextArea(1, 3)]
    public string[] dialogLines;//对话文字数组
    [SerializeField]
    private int currentLine;//记录对话文字行数
    private bool isScrolling;
    [SerializeField] private float textSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        dialogText.text = dialogLines[currentLine];//对话框显示文字某一行内容
    }

    private void Update()
    {
        if (dialogBox.activeInHierarchy)//检测对话窗口是否为激活状态
        {
            if (Input.GetMouseButtonUp(0))//释放左键，0表示左键，1表示右键，2表示中键
            {
                if (isScrolling == false)
                {

                    currentLine++;
                    if (currentLine < dialogLines.Length)//检测当前句子的行数是否超过文字数组的长度
                    {
                        CheckName();
                        //dialogText.text = dialogLines[currentLine];//对话框显示文字某一行内容
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogBox.SetActive(false);//对话框关闭
                        FindObjectOfType<PlayerMovement>().canMove = true;//人物移动开启
                    }
                }

            }
            // else
            // {
            //     CheckName();
            //     StartCoroutine(ScrollingText());
            // }

        }
    }

    public void ShowDialogue(string[] _newLines, bool _hasName)
    {
        dialogLines = _newLines;
        currentLine = 0;

        CheckName();

        //dialogText.text = dialogLines[currentLine];//对话框显示文字某一行内容
        StartCoroutine(ScrollingText());

        dialogBox.SetActive(true);

        nameText.gameObject.SetActive(_hasName);//如果hasname=true，nameText会显示；反之隐藏


        FindObjectOfType<PlayerMovement>().canMove = false;//人物移动关闭
    }

    private void CheckName()//检测是否有人名
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", "");//“ ”替换“n-”
            currentLine++;
        }
    }

    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogText.text = "";
        foreach (char letter in dialogLines[currentLine].ToCharArray())
        {
            dialogText.text += letter;//逐一显示字母
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }
}
