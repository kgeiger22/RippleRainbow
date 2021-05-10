using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI Instance;

    private float fadeDuration = 1f;
    private CanvasGroup canvasGroup;
    
    void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeInUI()
    {
        StopAllCoroutines();
        StartCoroutine("FadeIn");
    }

    public void FadeOutUI()
    {
        StopAllCoroutines();
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        float r;
        float currentTime = 0;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            r = Mathf.Lerp(0, 1, currentTime / fadeDuration);
            canvasGroup.alpha = r;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    IEnumerator FadeOut()
    {
        float r;
        float currentTime = 0;
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            r = Mathf.Lerp(1, 0, currentTime / fadeDuration);
            canvasGroup.alpha = r;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}
