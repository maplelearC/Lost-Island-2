using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Strat : MonoBehaviour
{
   

    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    //MainUIÒÆ¶¯
    void Move()
    {

        float step = 100 * Time.deltaTime;
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(0, 0, 0), step);
        
            if(Input.GetKeyDown(KeyCode.Q))
            {
                gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
       

    }   
}
