using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    private readonly string highscoreKey = "HighScore";

    private void Awake()
    {
        SingletonPattern();
    }

    public void SetHighScore()
    {
        int score = ActivityScoreManager.instance.GetScore();

        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt(highscoreKey, score);
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(highscoreKey);
    }

    private void SingletonPattern()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
