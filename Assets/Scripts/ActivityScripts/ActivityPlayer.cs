/*
* Author: Hazel
* Date: 2025-12-08
* Description: Controls the player character's movement and interactions in the activity.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActivityPlayer : MonoBehaviour
{
    Vector3 InputPosition; // Position of the input (touch or mouse)
    bool touched; // Flag to indicate if the player is being touched

    // Initialize player position
    private void Start() 
    {
        // Starting position
        transform.position = new Vector3(0, 0, 2); 
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for input start and end
        if (InputStarted())
        {
            touched = true;
        }
        else if (InputEnded())
        {
            touched = false;
        }

        // Move player if being touched
        if (touched)
        {
            MovePlayer();
        }
    }

    // Move the player towards the input position
    void MovePlayer()
    {
        // Calculate the target position based on input
        Vector3 targetPosition = transform.position;
        // Get input position
        if (TouchInput())
        {
            InputPosition = GetCursorPosition(Input.GetTouch(0).position);
        }
        else
        {
            InputPosition = GetCursorPosition(Input.mousePosition);
        }
        // Update target position's x coordinate
        targetPosition.x = InputPosition.x;

        // Move towards the target position
        float step = 30 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    // Convert screen input position to world position
    Vector3 GetCursorPosition(Vector3 input)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(input.x, input.y, input.z));
    }

    // Check if input has started (touch or mouse)
    private bool InputStarted()
    {
        return TouchInput() || MouseInput();
    }

    // Check if input has ended (touch or mouse)
    private bool InputEnded()
    {
        return TouchEnded() || MouseEnded();
    }

    // Check for touch input start
    private bool TouchInput()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }

    // Check for touch input end
    private bool TouchEnded()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                return true;
            }
        }
        return false;
    }

    // Check for mouse input start
    private bool MouseInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    // Check for mouse input end
    private bool MouseEnded()
    {
        return Input.GetMouseButtonUp(0);
    }
}
