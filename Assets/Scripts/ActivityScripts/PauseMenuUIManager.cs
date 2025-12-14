/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages the Pause Menu UI.
*/
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuUIManager : UIManager
{
    public static PauseMenuUIManager instance; // Singleton instance

    [SerializeField]
    private GameObject PauseButton; // Reference to the pause button

    [SerializeField]
    GameObject TapToContinue; // Reference to the Tap to Continue UI element

    // Initialize the singleton instance
    private void Awake()
    {
        SingletonPattern();
    }

    // Enable or disable the Pause Menu UI
    public override void Enable(bool active)
    {
        if(active)
        { 
            //Show Pause Menu UI
            base.Enable(active);
            ActivityScoreManager.instance.PauseScoreTimer(true);
            TapToContinue.SetActive(active);
            PauseButton.SetActive(false);
        }
        else
        {
            //Hide Pause Menu UI
            base.Enable(active);
            ActivityScoreManager.instance.PauseScoreTimer(false);
            TapToContinue.SetActive(active);
            PauseButton.SetActive(true);
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
