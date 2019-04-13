using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BezierCurvePointRenderer : MonoBehaviour {

    // Use this for initialization
    [Tooltip("constant update of given Points")]
    public bool updatePoints;
    [Tooltip("Line Renderer required")]
    public LineRenderer lineRenderer;
    public int vertexCount;
    [Tooltip("shown just for debugging, min 2 Points are required")]
    public Transform[] points;

    private Vector3 lastPoint;

	void Start () {
        
        points = gameObject.GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        if (updatePoints) {

            points = gameObject.GetComponentsInChildren<Transform>();
        }

        var pointList = new List<Vector3>();

        if (points.Length > 2) {

            for (int i = 0; i <= points.Length-1; i++) {

                if (i == 0 || i == points.Length-1) {
                    
                    pointList.Add(points[i].position);

                    lastPoint = points[i].position;

                }else {
                   
                    for (float ratio = 0.0f / vertexCount; ratio <= 1; ratio += 1.0f / vertexCount)
                    {

                        var tangentLineVertex1 = Vector3.Lerp(lastPoint, points[i].position, ratio);
                       
                        var tangentLineVertex2 = Vector3.Lerp(points[i].position, points[i+1].position, ratio);

                        var bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);

                        pointList.Add(bezierPoint);

                        lastPoint = bezierPoint;
                    }
                }
            }

        } else if (points.Length == 2){ 

            pointList.Add(points[0].position);
            pointList.Add(points[1].position);
        }

        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
	}
}
