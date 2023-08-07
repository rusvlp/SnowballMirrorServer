using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RouteViewDebug : MonoBehaviour
{
    [SerializeField] BezierCurve _curve;

    private void OnDrawGizmos()
    {
        for (float t = 0; t < 1; t += 0.05f)
        {
            Gizmos.DrawSphere(BezierTransport.AutoBezierPosition(t, _curve.Points), 0.1f);
            Gizmos.DrawLine(BezierTransport.AutoBezierPosition(t, _curve.Points), BezierTransport.AutoBezierPosition(t + 0.05f, _curve.Points));
        }
    }
}
