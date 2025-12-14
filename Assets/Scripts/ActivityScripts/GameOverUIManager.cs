/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages the Game Over UI elements.
*/
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameOverUIManager : UIManager
{
    private const string highScorePreText = "Your Highscore: "; // Prefix text for high score display
    private const string newHighScoreText = "New Highscore"; // Text to display for new high score
    public static GameOverUIManager instance; // Singleton instance

    [SerializeField]
    TextMeshProUGUI highscoreText; // UI element to display the high score

    [SerializeField]
    private GameObject PauseButton; // Reference to the pause button

    [SerializeField]
    private GameObject ScoreUI; // Reference to the score UI

    [SerializeField]
    TextMeshProUGUI scoreText; // UI element to display the final score

    // Initialize the singleton instance
    private void Awake()
    {
        SingletonPattern();
    }

    // Enable or disable the Game Over UI
    public override void Enable(bool active)
    {
        if(active)
        {
            //Show Game Over UI with score and high score
            base.Enable(active);
            highscoreText.SetText(GetHighScoreText());
            scoreText.SetText(ActivityScoreManager.instance.GetScore().ToString("D4"));
            highscoreText.gameObject.SetActive(active);
            PauseButton.SetActive(false);
            ScoreUI.SetActive(false);
        }
        else
        {
            //Hide Game Over UI
            highscoreText.gameObject.SetActive(active);
            base.Enable(active);
            PauseButton.SetActive(true);
            ScoreUI.SetActive(true);
        }
    }

    // Get the appropriate high score text to display
    private string GetHighScoreText()
    {
        // Retrieve current high score and player's score
        int highscore = HighScoreManager.instance.GetHighScore();
        int score = ActivityScoreManager.instance.GetScore();

        // Determine if the current score is a new high score
        if(score > highscore)
        {
            return newHighScoreText;
        }
        else
        {
            return highScorePreText + highscore.ToString();
        }
    }

    // Singleton pattern implementation
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
