using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    
    private bool isFade;
    public void Transition(string form, string to)
    {
        if (!isFade)
        {
            StartCoroutine(TransitionToScene(form, to));
        }
        
    }

    private IEnumerator TransitionToScene(string form, string to)
    {
        yield return Fade(1);
        yield return SceneManager.UnloadSceneAsync(form);
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);
        
        //设置新场景位激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        
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
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, taegetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;
        isFade = false;
    }
}