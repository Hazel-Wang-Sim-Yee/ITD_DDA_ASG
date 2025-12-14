/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages the Start Menu UI elements.
*/
using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class StartMenuUIManager : UIManager
{
    public static StartMenuUIManager instance; // Singleton instance

    [SerializeField]
    GameObject score; // Reference to the score UI element

    [SerializeField]
    private GameObject PauseButton; // Reference to the pause button

    // Initialize the singleton instance
    private void Awake()
    {
        SingletonPattern();
    }

    // Enable or disable the Start Menu UI
    public override void Enable(bool active)
    {
        if(active)
        {
            //Hide score and pause button when start menu is active
            base.Enable(active);
            score.SetActive(false);
            PauseButton.SetActive(false);
        }
        else
        {
            //Show score and pause button when start menu is inactive
            score.SetActive(true);
            base.Enable(active);
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
