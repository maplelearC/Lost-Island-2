using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreation : MonoBehaviour
{
    //����װ�γ�ʼ����ͼ����Ҫ���������
    //0.�ϼ� 1.ǽ 2.�ϰ� 3.����Ч�� 4.���� 5.�� 6. ����ǽ 7.���2
    public GameObject[] item;

    //�Ѿ��ж�����λ���б�
    private List<Vector3> itemPoditionList = new List<Vector3>();
    

    private void Awake()
    {
        InitMap();
    }
    private void InitMap()
    {
        //ʵ�����ϼ�
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //��ǽ��Χ�ϼ�
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //ʵ������Χǽ
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }
       



        //��������
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 4f, 5f);

        //ʵ������ͼ
        for (int I = 0; I < 60; I++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);

        }

        for (int I = 0; I < 20; I++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }


        for (int I = 0; I < 20; I++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }

        for (int I = 0; I < 20; I++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
        //��ʼ�����
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;
        go.GetComponent<Born>().playnum = 1;
       
    }
    private void CreateItem(GameObject CreateGameObject,Vector3 createPosition,Quaternion createRotion)
    {
        GameObject itemGo = Instantiate(CreateGameObject, createPosition, createRotion);
        itemGo.transform.SetParent(gameObject.transform);
        itemPoditionList.Add(createPosition);
    }

    //�����漴λ�õķ���
    private Vector3 CreateRandomPosition()
    {
        // ������x=-10��10�����У�y=-8��8�������е�λ��
        while(true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if(!HasThePosition(createPosition))
            {
                return createPosition;
            }
           
        }
    }
    //�����ж�λ���б����Ƿ������λ��
    private bool HasThePosition(Vector3 createPos)
    {
        for(int i=0;i<itemPoditionList.Count;i++)
        {
            if(createPos==itemPoditionList[i])
            {
                return true;
            }
                   
        }
        return false;
    }
    //�������˵ķ���
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if(num==0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if(num==1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3],EnemyPos, Quaternion.identity);
    }
}
