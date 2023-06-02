using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed;

    [SerializeField] public GameObject DropTower;
    [SerializeField] public GameObject GameManager;
    void Start()
    {
        speed = 6f;
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<Transform>().parent == null)
        {
            DropTower = GameObject.FindGameObjectWithTag("DropTower");
            GetComponent<Transform>().Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        List<GameObject> boxes = GameManager.GetComponent<GameManager>().Boxes;
        Debug.Log(boxes.Count);
        if (gameObject.tag == "BoundingBox")
        {
            speed = 0;
            if (boxes.Count<1)
            {
                if (collision.tag == "Platform")
                {
                    GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
                    gameObject.tag = "Top";
                    boxes.Add(gameObject);
                    GameManager.GetComponent<GameManager>().MoveUp();
                }
                else if (collision.tag == "Ground")
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                if (collision.tag == "Platform")
                {
                    Destroy(gameObject);
                }
                else if (collision.tag == "Ground")
                {
                    Destroy(gameObject);
                }
                else if (collision.tag == "Dropped")
                {
                    Destroy(gameObject);
                }
                else 
                {
                    if (collision.gameObject == boxes[boxes.Count - 1])
                    {
                        if (Mathf.Abs(collision.transform.position.x - gameObject.transform.position.x) < 1.2)
                        {
                            gameObject.tag = "Top";
                            boxes[boxes.Count - 1].tag = "Dropped";
                            boxes.Add(gameObject);
                            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
                            GameManager.GetComponent<GameManager>().MoveUp();

                        }
                        else
                        {
                            Destroy(gameObject);
                            if (Mathf.Abs(collision.transform.position.x - gameObject.transform.position.x) > 1.3)
                                if (boxes.Count >= 1)
                                {
                                    GameObject box = boxes[boxes.Count - 1];
                                    boxes.RemoveAt(boxes.Count - 1);
                                    Destroy(box);
                                    if (boxes.Count >= 1)
                                        boxes[boxes.Count - 1].tag = "Top";
                                }
                            if (Mathf.Abs(collision.transform.position.x - gameObject.transform.position.x) > 1.35)
                                if (boxes.Count >= 1)
                                {
                                    GameObject box = boxes[boxes.Count - 1];
                                    boxes.RemoveAt(boxes.Count - 1);
                                    Destroy(box);
                                    if (boxes.Count >= 1)
                                        boxes[boxes.Count - 1].tag = "Top";
                                }
                        }
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}   
