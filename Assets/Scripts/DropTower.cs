using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTower : MonoBehaviour
{
    private float RotateAngle = 30f;
    private float high = 35f;
    private float low = -35f;

    void Start()
    {
    }

    void Update()
    {
        Rot();
    }
    void Rot()
    {
        float angle = GetComponent<Transform>().localEulerAngles.z;
        angle = (angle > 180) ? angle - 360 : angle;
        RotateAroundPivot(RotateAngle * Time.deltaTime);
        if (angle > high)
        {
            RotateAngle = -30f;
        }
        else if (angle < low)
        {
            RotateAngle = +30f;
        }

    }
    void RotateAroundPivot(float angle)
    {
        Vector3 pivot = GetComponent<Transform>().position;
        GetComponent<Transform>().eulerAngles += new Vector3(0, 0, angle);
    }
}
