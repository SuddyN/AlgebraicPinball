using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText;
 
    void Start (){
        buttonText = GetComponentInChildren<Text>();
    }
 
    public void OnPointerEnter (PointerEventData eventData)
    {
        buttonText.fontSize += 6;
    }
 
    public void OnPointerExit (PointerEventData eventData)
    {
        buttonText.fontSize -= 6;
    }
}
