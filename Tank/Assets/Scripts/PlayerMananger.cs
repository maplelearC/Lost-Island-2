using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMananger : MonoBehaviour
{
    //属性值
    public int lifeValue1 = 3;
    public int lifeValue2 = 3;
    public int playerScore = 0;
    public int player2Score = 0;
    public bool isDead1;
    public bool isDead2;
    public bool isDefeat1;
    public bool isDefeat2;

    


    //引用
    public GameObject born;
    public Text PlayerScoreText;
    public Text PlayerValueText;
    public Text Player2ScoreText;
    public Text Player2ValueText;
    public GameObject isDefeatUI;

    //单例
    private static PlayerMananger instance;
    


    public static PlayerMananger Instance
    {
        get => instance; 
        set => instance = value;
    }
    
    private void Awake()
    {
        Instance = this;  
         

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDefeat1&&isDefeat2)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnTheMain", 3);
            return;
        } 
        else if (lifeValue2 < 0 && lifeValue1 < 0)
        {
            Invoke("ReturnTheMain", 3);
            isDefeat1 = true; 
            isDefeat1 = true;
            return;
            
        }
        if (isDead1)
        {
            Recover1();
        }
         if(isDead2)
        {
            Recover2();
        }
        PlayerScoreText.text = playerScore.ToString();
        PlayerValueText.text = lifeValue1.ToString();
        Player2ScoreText.text = player2Score.ToString();
        Player2ValueText.text = lifeValue2.ToString();
    }

    private void Recover1()
    {
        
        if (lifeValue1<=0)
        {
            //游戏失败，返回主界面
            isDefeat1 = true;
           
        }
        else
        {
            lifeValue1--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            go.GetComponent<Born>().playnum = 2;
            isDead1 = false;
        }
        

    }
    private void Recover2()
    {
        if (lifeValue2 <= 0)
        {
            //游戏失败，返回主界面
            isDefeat2 = true;

        }
        else
        {
            lifeValue2--;
            GameObject go2 = Instantiate(born, new Vector3(2.05f, -8, 0), Quaternion.identity);
            go2.GetComponent<Born>().playnum = 3;
            go2.GetComponent<Born>().createPlayer = true;
            isDead2 = false;
        }
    }
    private void ReturnTheMain()
    {
        SceneManager.LoadScene(0);
    }
}
