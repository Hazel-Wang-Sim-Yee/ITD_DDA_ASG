/*
* Author: Hazel
* Date: 2025-12-10
* Description: Toggles the button image between sleep and wake sprites.
*/
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    public Sprite sleepSprite; // Sprite for sleep state
    public Sprite wakeSprite; // Sprite for wake state

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        // Add listener to button click
        if (btn != null)
        {
            btn.onClick.AddListener(ChangeImage);
        }
    }

    // Change the button image between sleep and wake sprites
    public void ChangeImage()
    {
        Image img = GetComponent<Image>();
        // Toggle the sprite based on current state
        if (img != null)
        {
            if (img.sprite == wakeSprite)
            {
                img.sprite = sleepSprite;
            }
            else
            {
                img.sprite = wakeSprite;
            }
        }
    }
}

