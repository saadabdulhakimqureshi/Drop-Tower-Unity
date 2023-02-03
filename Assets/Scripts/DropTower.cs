using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropTower : MonoBehaviour
{
    private float RotateAngle = 30f;
    private float high = 35f;
    private float low = -35f;
    [SerializeField] public GameObject BoundingBox;
    [SerializeField] public GameObject Camera;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyUp("a"))
        {
            CreateNewBox();
            BoundingBox.GetComponent<Transform>().parent = null;
            BoundingBox.GetComponent<BoxCollider>().enabled = true;
            Destroy(gameObject);
        }
        
        Oscillation();
    }

    void CreateNewBox()
    {
        GameObject newObject = Instantiate(gameObject);
    }

    public void MoveUp()
    {
        GetComponent<Transform>().position += new Vector3(0f, 1f, 0);
        Camera.GetComponent<Transform>().position += new Vector3(0f, 1f, 0);
    }

    void ChangeAnglePositive()
    {
        RotateAngle = +60f;
    }
    void ChangeAngleNegative()
    {
        RotateAngle = -60f;
    }


    void Oscillation()
    {
        float angle = GetComponent<Transform>().localEulerAngles.z;
        angle = (angle > 180) ? angle - 360 : angle;

        if (angle > high)
        {
            ChangeAngleNegative();
        }
        else if (angle < low)
        {
            ChangeAnglePositive();
        }
        RotateAroundPivot(RotateAngle * Time.deltaTime); ;


    }

    

    void RotateAroundPivot(float angle)
    {
        Vector3 pivot = GetComponent<Transform>().position;
        GetComponent<Transform>().eulerAngles += new Vector3(0, 0, angle);
    }
}
