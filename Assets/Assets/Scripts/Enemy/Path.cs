using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints;
    [SerializeField]
    private bool alwaysDrawPath;
    [SerializeField]
    private bool drawAsLoop;
    [SerializeField]
    private bool drawNumbers;
    public Color debugColour = Color.white;

    private void Awake()
    {
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned to the path!");
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (alwaysDrawPath)
        {
            DrawPath();
        }
    }

    public void DrawPath()
    {
        if (waypoints == null || waypoints.Count < 2)
        {
            Debug.LogWarning("Not enough waypoints to draw a path.");
            return;
        }

        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] == null)
            {
                Debug.LogError($"Waypoint at index {i} is null!");
                continue;
            }

            GUIStyle labelStyle = new GUIStyle
            {
                fontSize = 30,
                normal = { textColor = debugColour != Color.black ? debugColour : Color.white }
            };

            if (drawNumbers)
                Handles.Label(waypoints[i].position, i.ToString(), labelStyle);

            // Draw lines between waypoints
            if (i > 0)
            {
                Gizmos.color = debugColour;
                Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
            }
        }

        // Draw loop line
        if (drawAsLoop && waypoints.Count > 1)
        {
            Gizmos.color = debugColour;
            Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position);
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (!alwaysDrawPath)
        {
            DrawPath();
        }
    }

    [ContextMenu("Populate Waypoints")]
    public void PopulateWaypoints()
    {
        waypoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            waypoints.Add(child);
        }
    }
#endif
}
