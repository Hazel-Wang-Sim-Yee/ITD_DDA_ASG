using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActivityPlayer : MonoBehaviour
{
    Vector3 InputPosition;
    bool touched;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 2);
    }
    private void Update()
    {
        if (InputStarted())
        {
            touched = true;
        }
        else if (InputEnded())
        {
            touched = false;
        }

        if (touched)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        Vector3 targetPosition = transform.position;
        if (TouchInput())
        {
            InputPosition = GetCursorPosition(Input.GetTouch(0).position);
        }
        else
        {
            InputPosition = GetCursorPosition(Input.mousePosition);
        }
        targetPosition.x = InputPosition.x;

        float step = 30 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    Vector3 GetCursorPosition(Vector3 input)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(input.x, input.y, input.z));
    }

    private bool InputStarted()
    {
        return TouchInput() || MouseInput();
    }

    private bool InputEnded()
    {
        return TouchEnded() || MouseEnded();
    }

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

    private bool MouseInput()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool MouseEnded()
    {
        return Input.GetMouseButtonUp(0);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player Collision Detected");
    }
}
