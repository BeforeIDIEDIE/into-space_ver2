using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework.Internal.Commands;
public class SceneTransform : MonoBehaviour
{
    [SerializeField] private Color original = Color.black;
    [SerializeField] private Color transformColor = Color.gray;
    [SerializeField] private float transitionDuration = 0.5f;
    [SerializeField] private float typeSpeed = 0.05f;
    [SerializeField]private Image imageComponent;
    [SerializeField] private Button skipButton;
    [SerializeField] private TMP_Text targetText;
    private bool isComplete = false;
    private string allText = "오랜 환경 파괴와 자원 고갈로 인해 더 이상 현재의 행성에서 생존할 수 없게 되었다. " +
        "\n\n행성은 현재 종말에 임박한 상황" +
        "\n\n유일하게 포기하지 않던 플레이어는 마지막 탐사선에서 희망의 실마리를 찾았다." +
        "\n\n몇 광년 떨어진 미지의 행성 Eden-25 에서 발견된 신호는 생명체가 존재할 가능성을 암시했다." +
        "\n\n그러나 Eden-25가 생존 가능한 환경인지, 적대적인 존재가 있는지," +
        "\n또는 거주지로 적합하지 않은 환경인지는 알 수 없다." +
        "\n\n플레이어는 이 위태로운 상황에서 미완성된 우주선을 타고 떠나며, 선택과 생존의 여정에 나서게 된다.";
    private Coroutine typingCoroutine;
    
    private void OnEnable()
    {
        StartCoroutine(TransitionColor());
        skipButton.enabled = false;
    }

    private void OnDisable()
    {
        imageComponent.color = original;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        targetText.text = "";
    }

    public void SkipTyping()
    {
        targetText.text = allText;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        
    }

    private IEnumerator TransitionColor()
    {
        float elapsedTime = 0f;
        Color startColor = imageComponent.color;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            imageComponent.color = Color.Lerp(startColor, transformColor, elapsedTime / transitionDuration);
            yield return null;
        }
        imageComponent.color = transformColor;
        typingText();
    }

    private void typingText()
    {
        if (isComplete)
        {
            targetText.text = allText; 
        }
        else
        {
            typingCoroutine = StartCoroutine(TypeText(allText));
            skipButton.enabled = true;
        }
    }

    private IEnumerator TypeText(string textToType)
    {
        targetText.text = ""; 

        foreach (char letter in textToType)
        {
            targetText.text += letter;
            isComplete = true;
            yield return new WaitForSeconds(typeSpeed); 
        }
        typingCoroutine = null;
    }
}
