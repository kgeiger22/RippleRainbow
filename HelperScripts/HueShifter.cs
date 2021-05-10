//Shifts the hue of a material based on the HueController's current hue

using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class HueShifter : MonoBehaviour
{
    public float HueMultiplier = 1f;
    Renderer imageRenderer;
    MaterialPropertyBlock block;

    Material material;
    private void Awake()
    {
        imageRenderer = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
        imageRenderer.GetPropertyBlock(block);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.IsPaused())
        {
            block.SetFloat("_HueShift", HueController.hue * HueMultiplier);
            imageRenderer.SetPropertyBlock(block);
        }
    }
}
