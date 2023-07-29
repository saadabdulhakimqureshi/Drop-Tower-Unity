using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent<float> loadingStatus;
    public GameObject splashScreen;
    [SerializeField] public GameObject UIManager;

    void Update()
    {
        if (splashScreen != null && splashScreen.activeSelf)
        {
            
            StartCoroutine(CloseSplashScreen());
        }

    }

    IEnumerator CloseSplashScreen()
    {
        //Debug.Log("Closing Splash Screen");
        yield return new WaitForSeconds(2f);
        
        splashScreen.SetActive(false);
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
            UIManager.GetComponent<UIManager>().UpdateSlider(progress);
            yield return null;
        }
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuAsynchronously());
    }

    IEnumerator LoadMainMenuAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(operation.progress);
            UIManager.GetComponent<UIManager>().UpdateSlider(progress);
            yield return null;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator RestartGameAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(operation.progress);
            UIManager.GetComponent<UIManager>().UpdateSlider(progress);
            yield return null;
        }
    }

    public void SetDifficulty(float rotationAngle, int tries)
    {
        PlayerPrefs.SetFloat("rotationAngle", rotationAngle);
        PlayerPrefs.SetInt("Tries", tries);
    }
}

