using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActivityScoreManager : MonoBehaviour
{
    public static ActivityScoreManager instance;

    public TextMeshProUGUI scoreText;
    private int score;

    public Slider timerSlider;

    public float sliderTimer;

    public bool stopTimer = false;

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

    void Start()
    {
        timerSlider.maxValue = sliderTimer;
        timerSlider.value = sliderTimer;
        StateMachine.instance.SwitchGameState(GameStates.Launching);
    }

    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
    }

    public void PauseScoreTimer(bool pause)
    {
        stopTimer = pause;
        if (!pause)
        {
            StartCoroutine(StartTheTimerTicker());
        }
    }

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

    public void AddScore()
    {
        score++;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreText()
    {
        scoreText.SetText(score.ToString());
    }

    public void LevelStart()
    {
        stopTimer = false;
        sliderTimer = timerSlider.maxValue;
        timerSlider.value = sliderTimer;
        StartTimer();
    }
}
