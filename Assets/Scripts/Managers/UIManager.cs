using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public Slider loadingSlider;
    [SerializeField] public Text score;
    [SerializeField] public Text highScore;
    [SerializeField] public Text tries;
    [SerializeField] public GameObject GameManager;
    [SerializeField] public GameObject LoseScreen;
    [SerializeField] public GameObject GameScreen;
    [SerializeField] public MainMenuManager MainMenuManager;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSlider(float value)
    {
        loadingSlider.value = value;
    }

    public void UpdateScore(int value)
    {
        Debug.Log(value);
        score.text = value.ToString();
    }

    public void UpdateHighScore(int value)
    {
        Debug.Log(value);
        highScore.text = value.ToString();
    }

    public void UpdateTries(int value)
    {
        Debug.Log(value);
        tries.text = value.ToString();
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void StopGame()
    {
        GameScreen.SetActive(false);
        LoseScreen.SetActive(true);
        Time.timeScale = 0f;
    }


    public void SetEasy()
    {
        if (MainMenuManager != null)
        {
            MainMenuManager.SetDifficulty(13f, 5);
        }
    }

    public void SetNormal()
    {
        if (MainMenuManager != null)
        {
            MainMenuManager.SetDifficulty(13f, 4);
        }
    }
    public void SetHard()
    {
        if (MainMenuManager != null)
        {
            MainMenuManager.SetDifficulty(15f, 3);
        }
    }
}
