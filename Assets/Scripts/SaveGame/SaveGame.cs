using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame: MonoBehaviour
{
    // Start is called before the first frame update
    public int HighScore;
    public int CurrentScore;

    void Start()
    {
        GetHighScore();
    }
    public void SetCurrentScore(int score)
    {
        if (CurrentScore > HighScore)
        {

            SetHighScore(CurrentScore);
            
        }
        
        CurrentScore = score;
        GameObject.Find("UIManager").GetComponent<UIManager>().UpdateScore(score);
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
        GameObject.Find("UIManager").GetComponent<UIManager>().UpdateHighScore(score);
    }


    public void GetHighScore() {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        GameObject.Find("UIManager").GetComponent<UIManager>().UpdateHighScore(HighScore);
    }

}
