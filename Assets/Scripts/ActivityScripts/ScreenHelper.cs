/*
* Author: Hazel
* Date: 2025-12-08
* Description: Helper class to calculate screen boundaries in world coordinates.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScreenHelper : MonoBehaviour
{
    public static float ScreenTop; // Top boundary of the screen in world coordinates
    public static float ScreenLeft; // Left boundary of the screen in world coordinates
    public static float ScreenRight; // Right boundary of the screen in world coordinates

    // Calculate screen boundaries on Awake
    private void Awake()
    {
        Vector3 cameraPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        ScreenTop = cameraPosition.y;
        ScreenLeft = cameraPosition.x - Camera.main.transform.localScale.x + 1.5f;
        ScreenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0)).x - 1.5f;
    }
}
