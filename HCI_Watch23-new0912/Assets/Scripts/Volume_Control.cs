using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Volume_Control : MonoBehaviour
{   
    public Vector3 center;
    public float radius=1.5f;
    public float startAngle=0f;
    public float endAngle=0f;
    public int resolution=100;
    private LineRenderer lineRenderer;
    Vector3 angle;
    // Start is called before the first frame update
    void Start(){
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void drawArc(){
        lineRenderer.positionCount = resolution + 1;

        float angleStep = (endAngle - startAngle) / resolution;
        float currentAngle = startAngle;

        for (int i = 0; i <= resolution; i++)
        {
            float x = radius * Mathf.Cos(Mathf.Deg2Rad * currentAngle);
            float y = radius * Mathf.Sin(Mathf.Deg2Rad * currentAngle);
            Vector3 position = new Vector3(x, y, 0f);

            lineRenderer.SetPosition(i, position);

            currentAngle += angleStep;
        }
    }
}
