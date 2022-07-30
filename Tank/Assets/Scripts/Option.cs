using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    private int choice = 1;
    public Transform posOne;
    public Transform posTwo;
    public GameObject mainUI;
    public GameObject Play2prefab;

    public object SceneMananger { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)&&transform.position==posTwo.position)
        {
            choice = 0;
            transform.position = posOne.position;

        }
        
        else if (Input.GetKeyDown(KeyCode.S) && transform.position == posOne.position)
        {
            choice = 2;
            transform.position = posTwo.position;
        }

        else if (Input.GetKeyDown(KeyCode.W)&& transform.position == posOne.position)
       {
            choice = 0;
            transform.position = posTwo.position;
        }
        else if (Input.GetKeyDown(KeyCode.S) && transform.position == posTwo.position)
       {
            choice = 2;
            transform.position = posOne.position;
       }


        if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
        { 
            
                SceneManager.LoadScene(1);
                Play2prefab.SetActive(false);
            
        }

        else if (choice == 2 && Input.GetKeyDown(KeyCode.Space))
        {
            
                SceneManager.LoadScene(2);
               Play2prefab.SetActive(true);
        }
    }
}
