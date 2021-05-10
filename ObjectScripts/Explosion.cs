//Explosion script when the player presses the screen or a ball explodes

using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //The amount scale will be multiplied during explodion
    public float ExplodeSize = 3f;
    //The duration of the explosion in seconds
    public static float ExplodeDuration = 1f;

    public static float RotationSpeed = 120f;

    private float explosionCurrentTime;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private float startingScale;

    private int[] notes = new int[]
    {
        0,
        2,
        4,
        5,
        7,
        9,
        11,
        12,
        14,
        16,
        18,
        19,
        21,
        23,
        25,
        26,
    };

    private int[] notes_minor = new int[]
    {
        0,
        2,
        3,
        5,
        7,
        8,
        10,
        12,
        14,
        16,
        17,
        19,
        21,
        22,
        24,
        26,
    };
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if (!GameController.Instance.IsSFXMuted)
        {
            int note = Mathf.Clamp(GameController.Instance.combo, 0, 7);
            audioSource.pitch = Mathf.Pow(2, (notes_minor[note + 7] - 12) / 12.0f);
            audioSource.Play();
        }
        StartCoroutine("Explode");
    }

    IEnumerator Explode()
    {
        startingScale = transform.localScale.x;
        explosionCurrentTime = 0;

        while (explosionCurrentTime < ExplodeDuration)
        {
            while (GameController.Instance.IsPaused()) { yield return null; }

            explosionCurrentTime += Time.deltaTime;
            float ratio = Mathf.Min(explosionCurrentTime / ExplodeDuration, 1f);
            float scaleRatio = 1 - ((ratio - 1) * (ratio - 1) * (ratio - 1) * (ratio - 1));
            transform.localScale = new Vector3(Mathf.Lerp(startingScale, startingScale * ExplodeSize, scaleRatio), Mathf.Lerp(startingScale, startingScale * ExplodeSize, scaleRatio), 1f);
            float opacityRatio = 1 - ratio;
            MathHelper.SetSpriteAlpha(spriteRenderer, opacityRatio);
            transform.Rotate(0, 0, RotationSpeed * Time.deltaTime * opacityRatio);
            yield return null;
        }

        Destroy(gameObject);
    }
}
