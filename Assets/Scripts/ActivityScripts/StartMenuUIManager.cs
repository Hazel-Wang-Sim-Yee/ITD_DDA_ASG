using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class StartMenuUIManager : UIManager
{
    public static StartMenuUIManager instance;

    [SerializeField]
    GameObject score;

    [SerializeField]
    private GameObject PauseButton;

    private void Awake()
    {
        SingletonPattern();
    }

    public override void Enable(bool active)
    {
        if(active)
        {
            base.Enable(active);
            score.SetActive(false);
            PauseButton.SetActive(false);
        }
        else
        {
            score.SetActive(true);
            base.Enable(active);
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
