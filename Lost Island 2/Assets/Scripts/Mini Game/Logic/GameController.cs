using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent OnFinish;

    [Header("游戏数据")]
    public GameH2A_SO gameData;
    public GameH2A_SO[] gameDataArray;
    public GameObject lineParent;
    public LineRenderer linePrefab;
    public Ball ballPrefab;

    public Transform[] holderTransform;

    void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;
    }

    void OnDisable()
    {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
    }

    /*void Start()
    {
        DrawLine();
        CreateBall();
    }*/

    private void OnCheckGameStateEvent()
    {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch)
            {
                return;
            }
        }

        Debug.Log("Game Over");

        foreach (var holder in holderTransform)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }
        EventHandler.CallGamePassEvent(gameData.gameName);
        OnFinish?.Invoke();
    }

    public void ResetaGame()
    {
        /*for (int i = 0; i < lineParent.transform.childCount; i++)
        {
            Destroy(lineParent.transform.GetChild(i).gameObject);
        }*/

        foreach (var holder in holderTransform)
        {
            if (holder.childCount > 0)
            {
                Destroy(holder.transform.GetChild(0).gameObject);
            }
        }

        //DrawLine();
        CreateBall();
    }

    public void DrawLine()
    {
        foreach (var conections in gameData.lineConections)
        {
            var line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0, holderTransform[conections.form].position);
            line.SetPosition(1, holderTransform[conections.to].position);

            //创建每个Holder的连接关系
            holderTransform[conections.form]
                .GetComponent<Holder>()
                .linkHolders.Add(holderTransform[conections.to].GetComponent<Holder>());
            holderTransform[conections.to]
                .GetComponent<Holder>()
                .linkHolders.Add(holderTransform[conections.form].GetComponent<Holder>());
        }
    }

    public void CreateBall()
    {
        for (int i = 0; i < gameData.startBallOrder.Count; i++)
        {
            if (gameData.startBallOrder[i] == BallName.None)
            {
                holderTransform[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }
            Ball ball = Instantiate(ballPrefab, holderTransform[i]);

            holderTransform[i].GetComponent<Holder>().CheckBall(ball);
            holderTransform[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gameData.GetBallDetails(gameData.startBallOrder[i]));
        }
    }

    public void StartGameWeekData(int week)
    {
        gameData = gameDataArray[week];
        DrawLine();
        CreateBall();
    }
}
