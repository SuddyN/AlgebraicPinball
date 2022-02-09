using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionResponse : MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image background;

    private bool isMultiselect;
    
    [SerializeField] private Graphic singleSelectIcon;
    [SerializeField] private Graphic multiSelectIcon;
    
    [SerializeField] private Image resultSprite;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite incorrectSprite;

    public bool IsSelected() => toggle.isOn;
    
    public void Initialize(string value, ToggleGroup toggleGroup)
    {
        label.text = value;
        toggle.group = toggleGroup;
        isMultiselect = toggleGroup == null;
        singleSelectIcon.gameObject.SetActive(!isMultiselect);
        multiSelectIcon.gameObject.SetActive(isMultiselect);

        toggle.isOn = false;
        
        toggle.graphic = isMultiselect ? multiSelectIcon : singleSelectIcon;
    }

    public void Disable()
    {
        toggle.interactable = false;
    }

    public void Enable()
    {
        toggle.interactable = true;
    }

    public void Reset()
    {
        toggle.isOn = false;
        HideResultSprite();
    }

    public void ShowResultSprite(bool wasCorrect)
    {
        toggle.isOn = false;
        resultSprite.sprite = wasCorrect ? correctSprite : incorrectSprite;
        resultSprite.color = wasCorrect ? new Color(0.4f, 0.8f, 0.4f) : new Color(0.8f, 0.4f, 0.4f);
        resultSprite.gameObject.SetActive(true);
    }

    public void HideResultSprite()
    {
        resultSprite.gameObject.SetActive(false);
    }
}
