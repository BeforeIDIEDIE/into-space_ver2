using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrcStructure : StructureBase
{
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            StartCoroutine(PerformAction());
        }
    }
    public override IEnumerator PerformAction()
    {
        isPerformingAction = true;
        Debug.Log("작업 시작!");
        GameManager.Instance.SetInteractionState(InteractionType.Src, true);
        float elapsedTime = 0f;
        while ((elapsedTime < GameManager.Instance.GetproductSrcTime())&& (Input.GetKey(KeyCode.Space)))
        {
            if (!isNear)//플레이어가 감지 영역을 벗어난 경우
            {
                Debug.Log("작업 중단");
                GameManager.Instance.SetInteractionState(InteractionType.Src, false);
                isPerformingAction = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("작업 완료!");
        GameManager.Instance.AddSrc(GameManager.Instance.GetCurAddSrc());
        GameManager.Instance.SetInteractionState(InteractionType.Src, false);
        isPerformingAction = false;

        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//작업반복
        }
    }
}

