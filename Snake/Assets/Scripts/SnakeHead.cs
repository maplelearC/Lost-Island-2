using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SnakeHead : MonoBehaviour
{
    public List<RectTransform> bodyList = new List<RectTransform>();
    public float velocity = 0.35f;
    public int step;
    private int x;
    private int y;
    private Vector3 HeadPos;
    private Transform canvas;
    private bool isDie;

    public AudioClip eatClip;
    public AudioClip dieClip;
    public GameObject dieEffect;
    public GameObject bodyPrefab;
    public Sprite[] bodySprites = new Sprite[2];

    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        //ͨ��Resources.Load(string path)����������Դ��path����д����Ҫ��Resources / �Լ��ļ���չ��
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh02"));
        bodySprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01", "sb0201"));
        bodySprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0202"));
    }
    void Start()
    {
        InvokeRepeating("Move", 0, velocity);
        x = 0;y = step;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&MainUIController.Instance.isPause==false&&isDie==false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity-0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && MainUIController.Instance.isPause == false && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity );
        }
        if (Input.GetKey(KeyCode.W)&&y!=-step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0;
            y = step;
        }
        if(Input.GetKey(KeyCode.S) && y != step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        } 
        if(Input.GetKey(KeyCode.A) && x != step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step;
            y = 0;
        }
        if(Input.GetKey(KeyCode.D) && x != -step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step;
            y = 0;
        }
    }
    void Move()
    {
        HeadPos = gameObject.transform.localPosition;                                              //����������ͷ�ƶ�ǰ��λ��
        gameObject.transform.localPosition = new Vector3(HeadPos.x + x, HeadPos.y + y, HeadPos.z); //��ͷ������λ���ƶ�
        if (bodyList.Count > 0)
        {
            //����˫ɫ�����˷�������
        //bodyList.Last().localPosition = HeadPos;                                                 //����β�ƶ�����ͷ�ƶ�ǰ��λ��
        //bodyList.Insert(0, bodyList.Last());                                                     //����β��List�е�λ�ø��µ���ǰ
        //bodyList.RemoveAt(bodyList.Count - 1);                                                   //�Ƴ�List��ĩβ����β����

            //������˫ɫ����Ϊ�ﵽ��ʾĿ�ģ�ʹ�ô˷���
            for(int i=bodyList.Count-2; i>=0;i--)                                                  //�Ӻ���ǰ��ʼ�ƶ�����
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;                         //ÿһ�������ƶ�����ǰ��һ���ڵ��λ��
            }
            bodyList[0].localPosition = HeadPos;                                                   //��һ�������ƶ�����ͷ�ƶ�ǰ��λ��
        }
    }

    void Grow()
    {
        AudioSource.PlayClipAtPoint(eatClip, Vector3.zero);
        int Index =(bodyList.Count % 2 == 0)? 0:1;
        GameObject body = Instantiate(bodyPrefab,new Vector3(2000,2000,0),Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprites[Index];
        body.transform.SetParent(canvas,false);
        bodyList.Add((RectTransform)body.transform);
    }


    void Die()
    {
        AudioSource.PlayClipAtPoint(dieClip, Vector3.zero);
        CancelInvoke();
        isDie = true;
        Instantiate(dieEffect);
        PlayerPrefs.SetInt("lastl", MainUIController.Instance.length);
        PlayerPrefs.SetInt("lasts", MainUIController.Instance.score);
        if (PlayerPrefs.GetInt("bests", 0) < MainUIController.Instance.score)
        {
            PlayerPrefs.SetInt("bestl",  MainUIController.Instance.length);
            PlayerPrefs.SetInt("bests",  MainUIController.Instance.score);
        }
        StartCoroutine(GameOver(1.5f));
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Food"))//if (collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI();
            Grow();
            FoodMaker.Instance.MakeFood((Random.Range(0, 100) < 20) ? true : false);
        }
        else if (collision.gameObject.CompareTag("Reward"))
        {
            Destroy(collision.gameObject);
           MainUIController.Instance.UpdateUI(Random.Range(5,15)*10);
            Grow();
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            Die();
        }
        else
        {
            if(MainUIController.Instance.hasBorder)
            {
                Die();
            }
            else
            {
                switch (collision.gameObject.name)
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z);
                        break;
                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
                        break;
                    case "Left":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 210, transform.localPosition.y, transform.localPosition.z);
                        break;
                    case "Right":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 300, transform.localPosition.y, transform.localPosition.z);
                        break;

                }
            }

        }

    }
        
}

