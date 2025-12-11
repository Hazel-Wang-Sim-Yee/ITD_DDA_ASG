using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToggle : MonoBehaviour
{
    public Sprite sleepSprite;
    public Sprite wakeSprite;
    

    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(ChangeImage);
        }
    }

    public void ChangeImage()
    {
        Image img = GetComponent<Image>();
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

