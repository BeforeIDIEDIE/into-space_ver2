using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrcStructure : StructureBase
{
    private float actionDuration = 3f;
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

        float elapsedTime = 0f;


        while (elapsedTime < actionDuration)
        {
            if (!isNear)//플레이어가 감지 영역을 벗어난 경우
            {
                Debug.Log("작업 중단");
                isPerformingAction = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("작업 완료!");
        GameManager.Instance.AddSrc(GameManager.Instance.GetCurAddSrc());

        isPerformingAction = false;

        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//작업반복
        }
    }
}

