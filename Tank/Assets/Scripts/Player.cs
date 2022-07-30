using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //����ֵ
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private float timeVal;
    private float defendTimeVal=3;
    private bool isDefended=true;
    public GameObject defendEffectPrefab;

    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//�� �� �� ��
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
    { //������CD
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
        //�Ƿ����޵�״̬
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
        if(PlayerMananger.Instance.isDefeat1==true)
        {
            return;
        }
        Move();
       
    }


//̹�˵Ĺ�������
private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�ӵ������ĽǶȣ���ǰ̹�˵ĽǶ�+�ӵ�Ӧ����ת�ĽǶ�
            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
            timeVal = 0;
        }
    }
    //̹�˵��ƶ�����
    private void Move()
    {
        float v = Input.GetAxisRaw("player1V");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0||Input.GetKeyDown(KeyCode.S))
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0 || Input.GetKeyDown(KeyCode.W))
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
        float h = Input.GetAxisRaw("player1H");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0 || Input.GetKeyDown(KeyCode.A))
        {
            sr.sprite = tankSprite[3]; 
            bullectEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0 || Input.GetKeyDown(KeyCode.D))
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

    //̹�˵���������
    private void Die()
    {
        if(isDefended)
        {
            return;
        }
        
        PlayerMananger.Instance.isDead1 = true;
        //��ը��Ч
        Instantiate(explosionPrefab,transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }
}
