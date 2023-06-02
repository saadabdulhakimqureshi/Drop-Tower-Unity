using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DropTower : MonoBehaviour
{
    public float RotateAngle = 30f;
    public float high = 20f;
    public float low = -20f;
    public float WaitTime = 5f;
    public bool CanTakeInput = true;
    [SerializeField] public GameObject BoundingBox;
    [SerializeField] public GameObject Camera;

    void Start()
    {
      
    }

    void Update()
    {
        Oscillation();

    }


    public void Action()
    {
        Invoke("CreateNewBox", 1f);
        Drop();
        
    }


    void Drop()
    {
        BoundingBox.GetComponent<Transform>().parent = null;
        BoundingBox.GetComponent<BoxCollider>().enabled = true;
    }
    public void CreateNewBox()
    {
        GameObject newObject = Instantiate(GameObject.Find("CopyDropTower"));
        newObject.tag = "DropTower";
        newObject.GetComponent<DropTower>().RotateAngle = RotateAngle;
        newObject.GetComponent<Transform>().position = gameObject.GetComponent<Transform>().position;
        newObject.GetComponent<DropTower>().CanTakeInput = false;
        Destroy(gameObject);
    }

   

    void ChangeAnglePositive()
    {
        RotateAngle = +30f;
    }
    void ChangeAngleNegative()
    {
        RotateAngle = -30f;
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
