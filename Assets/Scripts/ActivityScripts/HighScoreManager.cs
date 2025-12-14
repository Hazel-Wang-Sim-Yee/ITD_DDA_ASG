/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages high scores for the activity.
*/
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance; // Singleton instance

    private readonly string highscoreKey = "HighScore"; // Key for PlayerPrefs

    // Initialize singleton instance
    private void Awake()
    {
        SingletonPattern();
    }

    // Set the high score if the current score is greater
    public void SetHighScore()
    {
        int score = ActivityScoreManager.instance.GetScore();

        // Update high score if current score is higher
        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt(highscoreKey, score);
        }
    }

    // Get the current high score
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(highscoreKey);
    }

    // Singleton pattern implementation
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
