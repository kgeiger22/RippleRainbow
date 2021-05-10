//Basic level entity

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[System.Serializable]
public class LevelData
{
    public short BestScore;
    public short seed;
}

public class Level : MonoBehaviour
{
    public LevelData data;

    public void TransIn(float waitDelay = 0)
    {
        foreach (Barrier barrier in transform.GetComponentsInChildren<Barrier>())
        {
            barrier.TransIn(waitDelay);
        }
    }

    public void TransOut(float waitDelay = 0)
    {
        foreach (Barrier barrier in transform.GetComponentsInChildren<Barrier>())
        {
            barrier.TransOut(waitDelay);
        }

        Destroy(gameObject, waitDelay + Barrier.TransitionDuration);
    }
}