using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text targetText; 
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.green; 

    private void Start()
    {
        targetText.color = normalColor;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetText.color = normalColor;
    }

    private void OnDisable()
    {
        targetText.color = normalColor;
    }
}

