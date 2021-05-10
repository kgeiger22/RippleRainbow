//Barrier script for the non-ball objects that the balls bounce off of

using System.Collections;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public static float TransitionDuration = 0.5f;

    private Vector3 startScale;

    private void Awake()
    {
        startScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 1);
    }

    public void TransIn(float waitDelay)
    {
        StartCoroutine(CoroutineTransIn(waitDelay));
    }

    public void TransOut(float waitDelay)
    {
        StartCoroutine(CoroutineTransOut(waitDelay));
    }

    IEnumerator CoroutineTransIn(float waitDelay)
    {
        yield return new WaitForSeconds(waitDelay);

        float currentTime = 0;
        Vector3 endScale = startScale;
        Vector3 currentScale = new Vector3(0, 0, 1);
        Vector3 endEuler = transform.rotation.eulerAngles;
        Vector3 currentEuler = endEuler;
        bool canSpin = GetComponent<Spinnable>() == null;
        float r;
        while (currentTime < TransitionDuration)
        {
            while (GameController.Instance.IsPaused()) { yield return null; }

            currentTime += Time.deltaTime;
            r = Mathf.Clamp(currentTime / TransitionDuration, 0, 1);
            r = MathHelper.RatioSwappedExponential(r);
            currentScale.x = r * endScale.x;
            currentScale.y = r * endScale.y;
            transform.localScale = currentScale;
            if (canSpin)
            {
                currentEuler.z = endEuler.z - 45 * (1 - r);
                transform.eulerAngles = currentEuler;
            }
            yield return null;
        }
    }

    IEnumerator CoroutineTransOut(float waitDelay)
    {
        yield return new WaitForSeconds(waitDelay);

        float currentTime = 0;
        Vector3 startScale = transform.localScale;
        Vector3 currentScale = new Vector3(0, 0, 1);
        Vector3 startEuler = transform.rotation.eulerAngles;
        Vector3 currentEuler = startEuler;
        bool canSpin = GetComponent<Spinnable>() == null;
        float r;
        while (currentTime < TransitionDuration)
        {
            while (GameController.Instance.IsPaused()) { yield return null; }

            currentTime += Time.deltaTime;
            r = Mathf.Clamp(currentTime / TransitionDuration, 0, 1);
            r = MathHelper.RatioExponential(r);
            currentScale.x = (1 - r) * startScale.x;
            currentScale.y = (1 - r) * startScale.y;
            transform.localScale = currentScale;
            if (canSpin)
            {
                currentEuler.z = startEuler.z + 45 * r;
                transform.eulerAngles = currentEuler;
            }
            yield return null;
        }
    }
}
