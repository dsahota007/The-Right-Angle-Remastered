using UnityEngine;
using System.Collections.Generic;

public class PlayerTrail : MonoBehaviour
{
    public LineRenderer lineRenderer;  // ✅ Assign this in the Inspector
    public float minDistance = 0.1f;   // ✅ How far the player must move before adding a new point
    public float trailLifetime = 1.5f; // ✅ How long before the trail disappears

    private List<Vector3> trailPoints = new List<Vector3>(); // Stores trail positions
    private List<float> pointTimes = new List<float>(); // Stores when each point was added

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 0; // Start with an empty trail
        lineRenderer.startWidth = 0.1f; // ✅ Adjust width in the Inspector as needed
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        // ✅ Only add a new trail point if the player has moved a minimum distance
        if (trailPoints.Count == 0 || Vector3.Distance(trailPoints[trailPoints.Count - 1], transform.position) > minDistance)
        {
            AddTrailPoint();
        }

        RemoveOldPoints(); // ✅ Removes points that are too old
    }

    void AddTrailPoint()
    {
        trailPoints.Add(transform.position);
        pointTimes.Add(Time.time); // Store time the point was added
        lineRenderer.positionCount = trailPoints.Count;
        lineRenderer.SetPosition(trailPoints.Count - 1, transform.position);
    }

    void RemoveOldPoints()
    {
        while (trailPoints.Count > 0 && Time.time - pointTimes[0] > trailLifetime)
        {
            trailPoints.RemoveAt(0);
            pointTimes.RemoveAt(0);
            lineRenderer.positionCount = trailPoints.Count;

            for (int i = 0; i < trailPoints.Count; i++)
            {
                lineRenderer.SetPosition(i, trailPoints[i]);
            }
        }
    }
}
