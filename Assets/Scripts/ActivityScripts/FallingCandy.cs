/*
* Author: Hazel
* Date: 2025-12-08
* Description: Handles the behavior of falling candy objects.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FallingCandy : MonoBehaviour
{
    private const int targetY = 20; // Y position where candy is considered off-screen
    Vector3 target; // Target position for the candy to move towards

    // Initialize the candy's target position
    private void Awake()
    {
        Reset();
    }

    // Reset the candy's position to start falling from the top
    public void Reset()
    {
        target = transform.position;
        target.y = -targetY;
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
    }

    // Move the candy downwards towards the target position
    private void MoveDown()
    {
        float step = 3f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    // Hide the candy and move it to the target position
    public void Hide()
    {
        transform.position = target;
        gameObject.SetActive(false);
    }

    // Deactivate the candy when it goes off-screen
    private void OnBecameInvisible()
    {
        if(transform.position.y > ScreenHelper.ScreenTop) { return;}
        gameObject.SetActive(false);
    }

    // Handle collision with the player
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player")) { return;} // Only respond to player collisions
        // Move candy to target position and deactivate

        transform.position = target;
        gameObject.SetActive(false);
        ActivityScoreManager.instance.AddScore();
    }
}
