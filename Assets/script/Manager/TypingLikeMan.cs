using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingLikeMan : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private float typeSpeed = 0.2f;
    private Coroutine typingCoroutine;
    private string fullText = "Into EDEN";
    private bool isCompleted = false; // 텍스트 출력 완료 여부

    private void OnEnable()
    {
        if (isCompleted)
        {
            displayText.text = fullText;
        }
        else
        {
            StartTyping();
        }
    }

    private void OnDisable()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }

    private void StartTyping()
    {
        displayText.text = "";
        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    private IEnumerator TypeText(string textToType)
    {
        displayText.text = "";
        
        foreach (char letter in textToType)
        {
            displayText.text += letter;
            isCompleted = true;
            yield return new WaitForSeconds(typeSpeed);
        }

        
        typingCoroutine = null; 
    }
}
