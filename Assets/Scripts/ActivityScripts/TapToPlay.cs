/*
* Author: Hazel
* Date: 2025-12-08
* Description: Handles player input to transition from the Menu state to the Playing state.
*/
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

public class TapToPlay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(PlayerInput())
        {
            HandleInput();
        }
    }

    // Check for player input (touch or mouse click)
    private bool PlayerInput()
    {
        return Input.touchCount > 0 || Input.GetMouseButtonDown(0);
    }

    // Handle the player input to switch game state
    private void HandleInput()
    {
        // Prevent state switch if a UI button was clicked
        if (UIButtonClicked()) { return; }
        StateMachine.instance.SwitchGameState(GameStates.Playing);
    }
#if UNITY_EDITOR
// Check if a UI button was clicked (for editor testing)
    private bool UIButtonClicked()
    {
        // Check if the mouse is over a UI element
        return EventSystem.current.IsPointerOverGameObject();
    }
#else
    // Check if a UI button was clicked (for touch input)
    private bool UIButtonClicked()
    {
        // Check if the touch is over a UI element
        if (Input.touches.Length < 1) { return false; }
        return EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId);
    }
#endif
}
