/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages the activity score and timer.
*/
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActivityScoreManager : MonoBehaviour
{
    public static ActivityScoreManager instance; // Singleton instance

    public TextMeshProUGUI scoreText; // UI element to display the score
    private int score; // Current score

    public Slider timerSlider; // UI slider for the timer

    public float sliderTimer; // Total time for the timer

    public bool stopTimer = false; // Flag to control timer state

    // Initialize the singleton instance
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize score and timer
        timerSlider.maxValue = sliderTimer;
        timerSlider.value = sliderTimer;
        StateMachine.instance.SwitchGameState(GameStates.Launching);
    }

    // Start the score timer
    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
    }

    // Pause or resume the score timer
    public void PauseScoreTimer(bool pause)
    {
        stopTimer = pause;
        if (!pause)
        {
            StartCoroutine(StartTheTimerTicker());
        }
    }

    // Coroutine to handle the timer countdown
    IEnumerator StartTheTimerTicker()
    {
        while (stopTimer == false)
        {
            sliderTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            if (sliderTimer <= 0)
            {
                StateMachine.instance.SwitchGameState(GameStates.GameOver);
                stopTimer = true;
            }

            if (stopTimer == false)
            {
                timerSlider.value = sliderTimer;
            }
        }
    }

    // Add score and update the UI
    public void AddScore()
    {
        score++;
        UpdateScoreText();
    }

    // Reset the score to zero
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Get the current score
    public int GetScore()
    {
        return score;
    }

    // Update the score text UI
    private void UpdateScoreText()
    {
        scoreText.SetText(score.ToString());
    }

    // Start the level by resetting timer and starting it
    public void LevelStart()
    {
        stopTimer = false;
        sliderTimer = timerSlider.maxValue;
        timerSlider.value = sliderTimer;
        StartTimer();
    }
}
