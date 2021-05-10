//Controls the game's hue which changes over time

using UnityEngine;

public class HueController : MonoBehaviour
{
    public static float hue;

    public float HueChangeRate = 10f;

    public Material UIMaterial; 

    private void Awake()
    {
        hue = 0;
    }

    private void Update()
    {
        if (!GameController.Instance.IsPaused())
        {
            hue += HueChangeRate * Time.deltaTime;
            if (UIMaterial != null)
            {
                UIMaterial.SetFloat("_HueShift", HueController.hue - 100f);
            }
        }
    }
}
