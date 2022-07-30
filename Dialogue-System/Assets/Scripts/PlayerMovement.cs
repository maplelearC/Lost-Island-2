using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [Range(1, 10)]
    public float moveSpeed = 3f;//速度
    private float moveV, moveH;
    public bool canMove = true;//是否可以移动

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveV = Input.GetAxis("Vertical") * moveSpeed;
        moveV = 0;
        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        if (canMove == true)
        {
            Flip();
        }

    }

    private void FixedUpdate()//移动
    {
        if (canMove == true)
        {
            rb.velocity = new Vector2(moveH, moveV);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Flip()//转向
    {
        if (moveH > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveH < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);//旋转180°
        }
    }
}
