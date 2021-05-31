using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCalculation : MonoBehaviour
{
    private void Update()
    {
       // Debug.Log(CalculateQuadraticBeziert(new Vector3(0.3f, 0f, 0f), new Vector3(0f, 0f, 0.5f), new Vector3(0.5f, 0, 0.5f), new Vector3(0.5f, 0f, 0f)));
    }

    //private void DrawQuadraticCurve()
    //{
    //    for (int i = 1; i < numPoints + 1; i++)
    //    {
    //        float t = i / (float)numPoints;
    //        positions[i - 1] = CalculateQuadraticBezierPoint(t, point0.position, point1.position, point2.position);
    //    }
    //    lineRenderer.SetPositions(positions);
    //}

    public Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        // return = (1-t)* itself * P0 + 2*(1-t)*t*P1 + t * itself * P2
        //            u                      u            tt
        //           uu * P0 + 2 * u * t * P1 + tt * P2
        
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p;
        p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    public float CalculateQuadraticBeziert(Vector3 p, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float[] t = new float[10];
        t[0] = (p0.x - p1.x - Mathf.Sqrt(p.x * p0.x - 2 * p.x * p1.x + p.x * p2.x - p0.x * p2.x + Mathf.Pow(p1.x, 2)))/(p0.x - 2 * p1.x + p2.x);
        t[1] = (p0.x - p1.x + Mathf.Sqrt(p.x * p0.x - 2 * p.x * p1.x + p.x * p2.x - p0.x * p2.x + Mathf.Pow(p1.x, 2))) / (p0.x - 2 * p1.x + p2.x);
        return t[0];
        
    }
}
