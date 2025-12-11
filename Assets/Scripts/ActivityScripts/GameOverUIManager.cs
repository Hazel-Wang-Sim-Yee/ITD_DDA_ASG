using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameOverUIManager : UIManager
{
    private const string highScorePreText = "Your Highscore: ";
    private const string newHighScoreText = "New Highscore";
    public static GameOverUIManager instance;

    [SerializeField]
    TextMeshProUGUI highscoreText;

    [SerializeField]
    private GameObject PauseButton;

    [SerializeField]
    private GameObject ScoreUI;

    [SerializeField]
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        SingletonPattern();
    }

    public override void Enable(bool active)
    {
        if(active)
        {
            base.Enable(active);
            highscoreText.SetText(GetHighScoreText());
            scoreText.SetText(ActivityScoreManager.instance.GetScore().ToString("D4"));
            highscoreText.gameObject.SetActive(active);
            PauseButton.SetActive(false);
            ScoreUI.SetActive(false);
        }
        else
        {
            highscoreText.gameObject.SetActive(active);
            base.Enable(active);
            PauseButton.SetActive(true);
            ScoreUI.SetActive(true);
        }
    }

    private string GetHighScoreText()
    {
        int highscore = HighScoreManager.instance.GetHighScore();
        int score = ActivityScoreManager.instance.GetScore();

        if(score > highscore)
        {
            return newHighScoreText;
        }
        else
        {
            return highScorePreText + highscore.ToString();
        }
    }

    void SingletonPattern()
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
