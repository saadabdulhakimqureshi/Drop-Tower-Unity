using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject DropTower;
    [SerializeField] public GameObject BoundingBox;
    [SerializeField] public GameObject Camera;
    public List<GameObject> Boxes;
    public bool CanTakeInput = true;

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        GetRefernces();
        if (CanTakeInput)
        {
            if (Input.GetKeyUp("a"))
            {
                CanTakeInput = false;
                Invoke("WaitForInput", 2f);
                
                DropTower.GetComponent<DropTower>().Action();

            }
        }

    }
    private void GetRefernces()
    {
        DropTower = GameObject.FindGameObjectWithTag("DropTower");
        Transform transform = DropTower.GetComponent<Transform>().GetChild(0);
        BoundingBox = transform.gameObject;
    }
    private void WaitForInput()
    {
        CanTakeInput = true;
    }

    public void MoveUp()
    {
        DropTower.GetComponent<Transform>().position += new Vector3(0f, BoundingBox.GetComponent<Renderer>().bounds.size.y, 0);
        Camera.GetComponent<Transform>().position += new Vector3(0f, BoundingBox.GetComponent<Renderer>().bounds.size.y, 0);
    }
}

