using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject player2Prefab;
    public int playnum;

    public GameObject[] enemyPrefabList;
    public bool createPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank",1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void BornTank()
    {
        if (createPlayer)
        {
            if (playnum == 1)
            {
                Instantiate(playerPrefab, transform.position, Quaternion.identity);
                Instantiate(player2Prefab, new Vector3(2.5f, -8, 0), Quaternion.identity);
            }
            else if(playnum==2)
            {
                Instantiate(playerPrefab, transform.position, Quaternion.identity);
            }   
            else if(playnum==3)
            {
                Instantiate(player2Prefab, new Vector3(2.05f, -8, 0), Quaternion.identity);
            }    

                
               

        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }

    }
}
