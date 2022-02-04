using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionResponse : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image background;
    
    [SerializeField] private Graphic singleSelectIcon;
    [SerializeField] private Graphic multiSelectIcon;
    [SerializeField] private Sprite singleSelectBackground;
    [SerializeField] private Sprite multiSelectBackground;

    public bool IsSelected() => toggle.isOn;
    
    public void Initialize(string value, ToggleGroup toggleGroup)
    {
        label.text = value;
        toggle.group = toggleGroup;
        singleSelectIcon.gameObject.SetActive(toggleGroup != null);
        multiSelectIcon.gameObject.SetActive(toggleGroup == null);

        toggle.isOn = false;
        
        if (toggleGroup == null)
        {
            toggle.graphic = multiSelectIcon;
            background.sprite = multiSelectBackground;
        }
        else
        {
            toggle.graphic = singleSelectIcon;
            background.sprite = singleSelectBackground;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Reset()
    {
        toggle.isOn = false;
    }
}
