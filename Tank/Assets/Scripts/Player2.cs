using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    //属性值
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float timeVal;
    private float defendTimeVal=3;
    private bool isDefended=true;
    public GameObject defendEffectPrefab;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//上 右 下 左
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;
   

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { //攻击的CD
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
        //是否处于无敌状态
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if(defendTimeVal<=0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
            
    }

    private void FixedUpdate()
    {
        if(PlayerMananger.Instance.isDefeat2==true)
        {
            return;
        }
        Move();
       
    }


//坦克的攻击方法
private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            timeVal = 0;
        }
    }
    //坦克的移动方法
    private void Move()
    {
        float v = Input.GetAxisRaw("player2V");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0 || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0||Input.GetKeyDown(KeyCode.RightArrow))
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }

        if(Mathf.Abs(v)>0.05f)
        {
            moveAudio.clip = tankAudio[1];
           
            if(!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

        if (v != 0)
        {
            return;
        }
        float h = Input.GetAxisRaw("player2H");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0 || Input.GetKeyDown(KeyCode.DownArrow))
        {
            sr.sprite = tankSprite[3]; 
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0 || Input.GetKeyDown(KeyCode.UpArrow))
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }
        if (Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];

            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }

    //坦克的死亡方法
    private void Die()
    {
        if(isDefended)
        {
            return;
        }
        
        PlayerMananger.Instance.isDead2 = true;
        //爆炸特效
        Instantiate(explosionPrefab,transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
}
