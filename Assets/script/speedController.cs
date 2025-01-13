using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class speedController : MonoBehaviour
{
    [SerializeField] private Toggle speedToggle;
    [SerializeField] private Sprite oneXSprite; 
    [SerializeField] private Sprite twoXSprite; 
    [SerializeField] private Image toggleImage;
    private bool isTwo;
    void Start()
    {
        Time.timeScale = 1.0f;
        isTwo = false;
        speedToggle.onValueChanged.AddListener(OnToggleSpeed);
        UpdateSpeedState(false);
    }

    private void OnToggleSpeed(bool isTwoX)
    {
        UpdateSpeedState(isTwoX);
    }

    private void UpdateSpeedState(bool isTwoX)
    {
        if (isTwoX)
        {
            Time.timeScale = 2f; // 2���
            isTwo = true;
            toggleImage.sprite = twoXSprite;
        }
        else
        {
            isTwo = false;
            Time.timeScale = 1f; // 1���
            toggleImage.sprite = oneXSprite;
        }
    }
    public bool GetIsTwo() => isTwo;
}
