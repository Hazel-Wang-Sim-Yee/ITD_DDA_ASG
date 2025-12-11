using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuUIManager : UIManager
{
    public static PauseMenuUIManager instance;

    [SerializeField]
    private GameObject PauseButton;

    [SerializeField]
    GameObject TapToContinue;

    private void Awake()
    {
        SingletonPattern();
    }

    public override void Enable(bool active)
    {
        if(active)
        {
            base.Enable(active);
            ActivityScoreManager.instance.PauseScoreTimer(true);
            TapToContinue.SetActive(active);
            PauseButton.SetActive(false);
        }
        else
        {
            base.Enable(active);
            ActivityScoreManager.instance.PauseScoreTimer(false);
            TapToContinue.SetActive(active);
            PauseButton.SetActive(true);
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
