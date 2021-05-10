using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class MathHelper
{
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public static bool IsInBounds(Vector3 point, Bounds bounds)
    {
        if (point.x < bounds.min.x || point.x > bounds.max.x) return false;
        if (point.y < bounds.min.y || point.y > bounds.max.y) return false;
        return true;
    }

    public static void SetImageAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public static void SetSpriteAlpha(SpriteRenderer sprite, float alpha)
    {
        Color color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

    public static float RatioExponential(float r)
    {
        return r * r;
    }

    public static float RatioSwappedExponential(float r)
    {
        return -(r - 1) * (r - 1) + 1;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
