using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoundingBox : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float distanceDestroy1;
    public float distanceDestroy2;
    public float distanceKeep;
    public bool landed;
    
    List<GameObject> boxes;

    [SerializeField] public SaveGame saveGame;
    [SerializeField] public DropTower DropTower;
    [SerializeField] public GameObject GameManager;
    public AudioSource DropSound;
    public AudioSource FailSound;
    void Start()
    {
        landed = false;
        boxes = GameManager.GetComponent<GameManager>().Boxes;
    }

    // Update is called once per frame
    void Update()
    {
        Drop();
        SetupRigidBodies();
    }

    void SetupRigidBodies()
    {
        if (boxes.Count > 3)
        {
            for (int i = boxes.Count - 4; i > -1; i--)
            {
                if (boxes[i] != null)
                {
                    Rigidbody rb = boxes[i].GetComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezeRotation;
                    //rb.isKinematic = true;
                }
            }

            for (int i = boxes.Count - 1; i > boxes.Count - 4; i--)
            {
                if (boxes[i] != null)
                {
                    Rigidbody rb = boxes[i].GetComponent<Rigidbody>();
                    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                }
                //rb.isKinematic = true;
            }
        }
    }
    void Drop()
    {

        if (GetComponent<Transform>().parent == null)
        {
            GetComponent<Transform>().Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision: "+ collision.collider.tag);
        speed = 0;

        if (!boxes.Contains(gameObject))
        {
            if (collision.collider.tag == "Platform")
            {
                if (boxes.Count > 1)
                {
                    GameManager.GetComponent<GameManager>().LoseTry();
                    if (!FailSound.isPlaying)
                        FailSound.Play();
                    Destroy(gameObject,1f);
                    
                }
                else
                {
                    AddTower();
                }
                
            }



            else if (collision.collider.tag == "Top")
            {
                if (Mathf.Abs(collision.transform.position.x - gameObject.transform.position.x) < distanceKeep)
                {
                    boxes[boxes.Count - 1].tag = "Dropped";
                    AddTower();
                    landed = true;
                }
            }

            else if (collision.collider.tag == "Ground")
            {
                GameManager.GetComponent<GameManager>().LoseTry();
                if (!FailSound.isPlaying)
                    FailSound.Play();
                Destroy(gameObject, 1f);
                
            }
        }
        else
        {
            if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
            {
                GameManager.GetComponent<GameManager>().LoseTry();
                if (!FailSound.isPlaying)
                    FailSound.Play();
                RemoveTower();

            }

        }
        UpdateScore(boxes.Count);
    }

    public void UpdateScore(int score)
    {
        saveGame.SetCurrentScore(score);
    }

    public void AddTower()
    {
        if (!landed)
        {
            if (!DropSound.isPlaying)
                DropSound.Play();
            DropTower.MoveUp(GetComponent<Renderer>().bounds.size.y);
            gameObject.tag = "Top";
            boxes.Add(gameObject);
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void RemoveTower()
    {
        boxes.Remove(gameObject);
        if (boxes.Count > 0)
            boxes[boxes.Count - 1].tag = "Top";

        DropTower.MoveDown(GetComponent<Renderer>().bounds.size.y);
        
        Destroy(gameObject, 1f);
    }
}
