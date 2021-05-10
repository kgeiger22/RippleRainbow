//Handles moving an object along a defined looping path

using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    public List<Vector3> Waypoints = new List<Vector3>();
    public float Speed = 1;
    public int StartingIndex = 0;

    private int currentPointIndex;

    private void Awake()
    {
        currentPointIndex = StartingIndex;
    }

    private void OnDrawGizmos()
    {
        if (Waypoints == null || Waypoints.Count < 2) return;
        Gizmos.color = new Color(0, 1, 1);
        for (int i = 0; i < Waypoints.Count; ++i)
        {
            Gizmos.DrawLine(Waypoints[i], Waypoints[(i + 1) % Waypoints.Count]);
        }
    }

    private void Update()
    {
        if (!GameController.Instance.IsPaused())
        {
            transform.position = Vector3.MoveTowards(transform.position, GetNextPoint(), Speed * Time.deltaTime);

            if ((GetNextPoint() - transform.position).sqrMagnitude < 0.001f)
            {
                MoveToNextTarget();
            }
        }
    }

    private void MoveToNextTarget()
    {
        currentPointIndex = GetNextPointIndex();
    }

    private int GetNextPointIndex()
    {
        return (currentPointIndex + 1) % Waypoints.Count;
    }

    private Vector3 GetNextPoint()
    {
        return Waypoints[GetNextPointIndex()];
    }
}
