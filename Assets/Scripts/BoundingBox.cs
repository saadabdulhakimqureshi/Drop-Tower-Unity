using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 3f;
    [SerializeField] public GameObject DropTower;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DropTower = GameObject.FindGameObjectWithTag("DropTower");
        if (GetComponent<Transform>().parent == null)
        {
            GetComponent<Transform>().Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Platform"){
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
            speed = 0;
            DropTower.GetComponent<DropTower>().MoveUp();
        }
        else if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Building")
        {
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
            speed = 0;
            DropTower.GetComponent<DropTower>().MoveUp();
        }

    }
}
