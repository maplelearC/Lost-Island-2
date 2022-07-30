using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullect : MonoBehaviour
{
    public float moveSpeed = 10;

    public bool isPlayerBullect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)//collision:触发到的游戏物体 
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullect)
                {
                   collision.SendMessage("Die");
                   Destroy(gameObject);
                }
               
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if(isPlayerBullect)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Barrier":
                if(isPlayerBullect)
                {
                    collision.SendMessage("PlayerAudio");
                    
                }
                Destroy(gameObject);
                break;
            default:
                break;
        }

    }
}
