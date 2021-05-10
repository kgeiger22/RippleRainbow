using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDisplay : MonoBehaviour
{
    public static GameDisplay Instance;

    private SpriteRenderer[] ClickSprites;
    private MaterialPropertyBlock[] ClickBlocks;
    private Meter meter;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        ClickSprites = new SpriteRenderer[GameController.Instance.numClicks];
        ClickBlocks = new MaterialPropertyBlock[GameController.Instance.numClicks];
        for (int i = 0; i < GameController.Instance.numClicks; ++i)
        {
            ClickBlocks[i] = new MaterialPropertyBlock();
            ClickSprites[i] = transform.Find("Click" + (i + 1).ToString()).GetComponent<SpriteRenderer>();
            ClickSprites[i].GetPropertyBlock(ClickBlocks[i]);
        }
        meter = transform.Find("Meter").GetComponent<Meter>();
    }

    public void SetMeter(float r)
    {
        meter.Set(r);
    }

    public void ResetDisplay()
    {
        meter.ResetMeter();
        for (int i = 0; i < GameController.Instance.numClicks; ++i)
        {
            StartCoroutine(FadeClick(i, false));
        }
    }

    public void DarkenClickIcon(int i)
    {
        if (i >= 0 && i < GameController.Instance.numClicks)
        {
            StartCoroutine(FadeClick(i, true));
        }
    }

    public void Fade(float duration, float startOpacity, float endOpacity)
    {
        FadeSpriteController[] sprites = GetComponentsInChildren<FadeSpriteController>();
        foreach (FadeSpriteController sprite in sprites)
        {
            sprite.Fade(duration, startOpacity, endOpacity);
        }
    }

    IEnumerator FadeClick(int i, bool darken)
    {
        float duration = 0.5f;
        float time = 0f;
        float startVal = ClickBlocks[i].GetFloat("_Val");
        if (startVal == 0) startVal = 1.3f;
        float endVal = darken ? 0.5f : 1.3f;

        while (duration > time)
        {
            while (GameController.Instance.IsPaused()) { yield return null; }

            time += Time.deltaTime;
            ClickBlocks[i].SetFloat("_Val", Mathf.Lerp(startVal, endVal, time / duration));
            ClickSprites[i].SetPropertyBlock(ClickBlocks[i]);
            yield return null;
        }
    }
}
