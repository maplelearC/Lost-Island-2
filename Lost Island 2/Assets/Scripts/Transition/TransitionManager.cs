using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    [SceneName]
    public string startScene;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;

    private bool isFade;
    private bool canTransition;

    void OnEnable()
    {
        EventHandler.GameStateChangedEvent += OnGameStateChangedEvent;
    }

    void OnDisable()
    {
        EventHandler.GameStateChangedEvent += OnGameStateChangedEvent;
    }

    private void OnGameStateChangedEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }

    void Start()
    {
        StartCoroutine(TransitionToScene(string.Empty, startScene));
    }

    public void Transition(string form, string to)
    {
        if (!isFade && canTransition)
        {
            StartCoroutine(TransitionToScene(form, to));
        }
    }

    private IEnumerator TransitionToScene(string form, string to)
    {
        yield return Fade(1);

        if (form != string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(form);
        }

        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        //设置新场景位激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);

        EventHandler.CallAfterSceneUnloadEvent();
        yield return Fade(0);
    }

    /// <summary>
    /// 淡入淡出场景
    /// </summary>
    /// <param name="taegetAlpha">1是黑，0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float taegetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - taegetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, taegetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(
                fadeCanvasGroup.alpha,
                taegetAlpha,
                speed * Time.deltaTime
            );
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}
