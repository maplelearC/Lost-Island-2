using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //����ֵ
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float h;
    private float v=-1;
   
    //private float defendTimeVal = 3;
    //private bool isDefended = true;
    //public GameObject defendEffectPrefab;

    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//�� �� �� ��
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;

    //��ʱ��
    private float timeVal;
    private float timevalChangeDirection;
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
    {
        //�Ƿ����޵�״̬
        //if (isDefended)
        //{
        //    defendEffectPrefab.SetActive(true);
        //    defendTimeVal -= Time.deltaTime;
        //    if (defendTimeVal <= 0)
        //    {
        //        isDefended = false;
        //        defendEffectPrefab.SetActive(false);
        //    }
        //}
        //������ʱ����
        if (timeVal >= 0.9f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();

    }


    //̹�˵Ĺ�������
    private void Attack()
    {
       
            //�ӵ������ĽǶȣ���ǰ̹�˵ĽǶ�+�ӵ�Ӧ����ת�ĽǶ�
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            timeVal = 0;
        
    }
    //̹�˵��ƶ�����
    private void Move()
    {
        if(timevalChangeDirection>=2)
        {
            int num = Random.Range(0, 8);
            if(num>5)
            {

                this.v = -1;
                this.h = 0;
            }
            else if(num==0)
            {
                this.v = 1;
                this.h = 0;
            }
            else if(num>0&&num<=2)
            {
                this.v = -1;
                this.h = 0;
            } else if(num>2&&num<=4)
            {
                this.v = 1;
                this.h = 0;
            }
            timevalChangeDirection = 0;
        }
        else
        {
            timevalChangeDirection += Time.fixedDeltaTime;
        }
       
        
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }

        if (v != 0)
        {
            return;
        }
       
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }

    }

    //̹�˵���������
    private void Die()
    {
        //if (isDefended)
        //{
        //    return;
        //}

        PlayerMananger.Instance.playerScore++;
        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy")
        {
            timevalChangeDirection = 4;
        }
    }
}

