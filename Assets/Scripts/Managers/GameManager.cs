using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject DropTower;
    [SerializeField] public GameObject BoundingBox;
    [SerializeField] public GameObject Camera;
    public AudioSource GameOver;
    public Image touchArea;
    public UIManager UIManager;
    public List<GameObject> Boxes;
    public bool CanTakeInput = true;
    public int tries;

    public UnityEvent<float> loadingStatus;

    void Start()
    {
        Time.timeScale = 1f;
        SetupLevel();
        
    }

    void SetupLevel()
    {
        float rotationAngle = PlayerPrefs.GetFloat("rotationAngle", 13f);
        tries = PlayerPrefs.GetInt("Tries", 3);
        UIManager.UpdateTries(tries);
        DropTower script = DropTower.GetComponent<DropTower>();
        script.RotateAngle = rotationAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (tries > 0)
        {
            GetRefernces();

            if ((Input.GetKeyUp("a") )&& CanTakeInput)
            {
                CanTakeInput = false;
                Invoke("WaitForInput", 2f);

                DropTower.GetComponent<DropTower>().Action();

            }

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.touches[i];

                    RectTransform imageRect = touchArea.GetComponent<RectTransform>();
                    // Convert the touch/click position to canvas space
                    Vector2 touchPos = touch.position;

                    Vector2 localTouchPosition = touch.position;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(imageRect, touchPos, null, out localTouchPosition);

                    // Check if the touch position is inside the image bounds
                    if (imageRect.rect.Contains(localTouchPosition))
                    {
                        Drop();
                        // Your logic for when the touch is inside the image goes here
                    }

                }
            }
        }
        else
        {
            StartCoroutine(StopGame());
           
        }

        

    }

    IEnumerator StopGame()
    {
        Debug.Log("Stopping Game!");
        if (GameMusic.Instance!=null)
        {
            GameMusic.Instance.GetComponent<AudioSource>().Stop();
        }
        if (!GameOver.isPlaying && GameOver.enabled)
        {
            GameOver.Play();
        }
        DropTower.GetComponent <DropTower>().RotateAngle = 0f;
        DropTower.GetComponent<DropTower>().BoundingBox.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        GameOver.enabled = false;
        UIManager.StopGame();
    }

    public void LoseTry()
    {
        tries -= 1;
        UIManager.UpdateTries(tries);
    }

    public void Drop()
    {
        if (CanTakeInput)
        {
            CanTakeInput = false;
            Invoke("WaitForInput", 2f);

            DropTower.GetComponent<DropTower>().Action();
        }
    }

    private void GetRefernces()
    {
        Transform boundingBoxTransform = DropTower.GetComponent<Transform>().GetChild(0);
        BoundingBox = boundingBoxTransform.gameObject;
    }
    private void WaitForInput()
    {
        CanTakeInput = true;
    }

    public void MoveUp(float height)
    {
        
        
    }

    public void StartGame()
    {
        StartCoroutine(StartGameAsynchronously());
    }

    IEnumerator StartGameAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(operation.progress);
            loadingStatus?.Invoke(progress);
            yield return null;
        }
    }

    public void StopDropTowers(bool pause)
    {
        DropTower.GetComponent<DropTower>().enabled = !pause;
    }
}

