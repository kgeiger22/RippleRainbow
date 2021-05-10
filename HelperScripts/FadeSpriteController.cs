//Fades a sprite to a given opacity over time

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FadeSpriteController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Fade(float duration, float startOpacity, float endOpacity)
    {
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine(duration, startOpacity, endOpacity));
    }

    IEnumerator FadeCoroutine(float duration, float startOpacity, float endOpacity)
    {
        Color color = spriteRenderer.color;
        float fTime = 0;
        float r;
        while (fTime < duration)
        {
            fTime += Time.deltaTime;
            r = Mathf.Lerp(startOpacity, endOpacity, fTime / duration);
            color.a = r;
            spriteRenderer.color = color;
            yield return null;
        }
    }
}
