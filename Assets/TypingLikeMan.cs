using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingLikeMan : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private float typeSpeed = 0.2f; 

    private void Start()
    {
        // 코루틴 실행
        StartCoroutine(DisplayIntroText());
    }

    private IEnumerator DisplayIntroText()
    {
        displayText.text = "";
        yield return StartCoroutine(TypeText("Into EDEN"));
    }

    private IEnumerator TypeText(string textToType)
    {
        displayText.text = ""; // 텍스트 초기화

        // 한 글자씩 텍스트 출력
        foreach (char letter in textToType)
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typeSpeed); 
        }
    }
}
