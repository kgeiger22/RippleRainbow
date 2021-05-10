//The bottom meter that shows your current level progress

using System.Collections;
using UnityEngine;

public class Meter : MonoBehaviour
{
    public static Meter Instance;

    SpriteRenderer MeterOutline;
    SpriteRenderer MeterOutlineGlow;
    SpriteRenderer MeterFill;
    SpriteRenderer MeterWhite;
    SpriteMask MeterMask;

    float target;

    private void Awake()
    {
        Instance = this;
        MeterOutline = transform.Find("MeterOutline").GetComponent<SpriteRenderer>();
        MeterOutlineGlow = MeterOutline.transform.Find("MeterOutlineGlow").GetComponent<SpriteRenderer>();
        MeterFill = transform.Find("MeterFill").GetComponent<SpriteRenderer>();
        MeterWhite = transform.Find("MeterWhite").GetComponent<SpriteRenderer>();
        MeterMask = transform.Find("MeterMask").GetComponent<SpriteMask>();
    }

    public void ResetMeter()
    {
        Set(0);
    }

    public void Set(float r)
    {
        target = r;
        StopCoroutine("ScaleMask");
        StartCoroutine("ScaleMask");

        if (r >= 1f)
        {
            StartCoroutine("ScaleOutlineGlow");
            if (!GameController.Instance.IsSFXMuted)
            {
                GameObject.Find("FinishLevelSound").GetComponent<AudioSource>().Play();
            }
        }
    }

    IEnumerator ScaleMask()
    {
        float duration = 0.8f;
        float time = 0f;
        Vector3 scale = MeterMask.transform.localScale;
        float start = scale.x;
        scale.x = target;
        MeterWhite.transform.localScale = scale;
        Color color = Color.white;
        color.a = 0.6f;
        float r;
        while (time < duration)
        {
            while (GameController.Instance.IsPaused()) { yield return null; }

            time += Time.deltaTime;
            r = MathHelper.RatioSwappedExponential(time / duration);
            scale.x = Mathf.Lerp(start, target, r);
            MeterMask.transform.localScale = scale;
            color.a = Mathf.Lerp(0.6f, 0, r);
            MeterWhite.color = color;
            yield return null;
        }
    }

    IEnumerator ScaleOutlineGlow()
    {
        float duration = 0.8f;
        float time = 0f;
        Vector3 scale = new Vector3(1, 1, 1);
        Color color = Color.white;
        float r;
        while (time < duration)
        {
            while (GameController.Instance.IsPaused()) { yield return null; }

            time += Time.deltaTime;
            r = MathHelper.RatioSwappedExponential(time / duration);
            scale.x = Mathf.Lerp(1, 1.15f, r);
            scale.y = Mathf.Lerp(1, 2f, r);
            MeterOutlineGlow.transform.localScale = scale;
            color.a = 1 - r;
            MeterOutlineGlow.color = color;
            yield return null;
        }
    }
}