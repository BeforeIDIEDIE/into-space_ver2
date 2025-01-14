using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SrcStructure : StructureBase
{
    [SerializeField] private Image progressImage_bottom;
    [SerializeField] private Image progressImage_top;
    [SerializeField] private Image progress_all;
    private void Start()
    {
        progressImage_bottom.gameObject.SetActive(false);
        progressImage_top.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(GameManager.Instance.IsGameOver())
        {
            return;
        }
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction && !isPerformingAction )
        {
            StartCoroutine(PerformAction());
        }
        progress_all.fillAmount = GameManager.Instance.GetSrc() / GameManager.Instance.GetMaxSrc();
    }
    public override IEnumerator PerformAction()
    {
        if (GameManager.Instance.IsGameOver())
        {
            yield break ;
        }
        isPerformingAction = true;
        Debug.Log("작업 시작!");
        GameManager.Instance.SetInteractionState(InteractionType.Src, true);
        float elapsedTime = 0f;

        progressImage_top.fillAmount = 0f;
        progressImage_bottom.gameObject.SetActive(true);
        progressImage_top.gameObject.SetActive(true);

        while ((elapsedTime < GameManager.Instance.GetproductSrcTime())&& (Input.GetKey(KeyCode.Space)))
        {
            if (!isNear|| GameManager.Instance.IsGameOver())//플레이어가 감지 영역을 벗어난 경우
            {
                Debug.Log("작업 중단");
                GameManager.Instance.SetInteractionState(InteractionType.Src, false);
                isPerformingAction = false;
                progressImage_bottom.gameObject.SetActive(false);
                progressImage_top.gameObject.SetActive(false);
                yield break;
            }

            elapsedTime += Time.deltaTime;
            progressImage_top.fillAmount = elapsedTime / GameManager.Instance.GetproductSrcTime();
            yield return null;
        }

        Debug.Log("작업 완료!");
        GameManager.Instance.AddSrc(GameManager.Instance.GetCurAddSrc());
        GameManager.Instance.SetInteractionState(InteractionType.Src, false);
        isPerformingAction = false;
        progressImage_bottom.gameObject.SetActive(false);
        progressImage_top.gameObject.SetActive(false);

        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction && !GameManager.Instance.IsGameOver())
        {
            StartCoroutine(PerformAction());//작업반복
        }
    }
}

