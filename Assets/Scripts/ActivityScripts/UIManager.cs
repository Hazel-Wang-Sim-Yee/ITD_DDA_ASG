/*
* Author: Hazel
* Date: 2025-12-08
* Description: Manages UI elements in the game.
*/
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Enables or disables all child UI elements
    public virtual void Enable(bool active)
    {
        if (active)
        {
            SetChilds(active);
        }
        else
        {
            SetChilds(active);
        }
    }

    // Sets the active state of all child GameObjects
    private void SetChilds(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}