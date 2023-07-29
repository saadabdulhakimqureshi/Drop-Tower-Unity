using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DropTower : MonoBehaviour
{
    public float RotateAngle;
    public float high;
    public float low;
    public float movingSpeed;
    public Vector3 movePosition;
    private float CurrentAngle;
    [SerializeField] public GameObject BoundingBox;
    [SerializeField] public GameObject Camera;

    void Start()
    {
        movePosition = transform.position;
        ChangeAnglePositive();
    }

    void Update()
    {
        if (transform.position != movePosition)
        {
            Move();
        }
        Oscillation();

    }


    public void Action()
    {
        StartCoroutine(CreateNewBox());
        Drop();
        
    }


    void Drop()
    {
        BoundingBox.GetComponent<Transform>().parent = null;
        BoundingBox.GetComponent<BoxCollider>().enabled = true;
        BoundingBox.GetComponent<Rigidbody>().isKinematic = false;
    }

    IEnumerator CreateNewBox()
    {
        GameObject newBox = Instantiate(BoundingBox, BoundingBox.transform.position, BoundingBox.transform.rotation);
        newBox.SetActive(false);
        newBox.transform.parent = transform;
        newBox.transform.localScale = BoundingBox.transform.localScale;
        yield return new WaitForSeconds(movingSpeed);
        newBox.SetActive(true);
        BoundingBox = newBox;
    }

   

    void ChangeAnglePositive()
    {
        CurrentAngle =  RotateAngle;
    }
    void ChangeAngleNegative()
    {
        CurrentAngle = -1* RotateAngle;
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
        RotateAroundPivot(CurrentAngle * Time.deltaTime); ;


    }

    void RotateAroundPivot(float angle)
    {
        Vector3 pivot = GetComponent<Transform>().position;
        GetComponent<Transform>().eulerAngles += new Vector3(0, 0, angle);
    }
    void Move() { 

        transform.position = Vector3.Lerp(transform.position, movePosition, movingSpeed * Time.deltaTime);
    }

    public void MoveUp(float height)
    {
        movePosition = movePosition + new Vector3(0f, height, 0); 
    }

    public void MoveDown(float height)
    {
        movePosition = movePosition - new Vector3(0f, height, 0);
    }
}
