using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    [SerializeField] private Transform tongueEnd;
    [SerializeField] private Transform tongueOrigin;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    private void FixedUpdate()
    {
        lineRenderer.SetPosition(0, tongueOrigin.position);
        lineRenderer.SetPosition(1, tongueEnd.position);
    }
}
