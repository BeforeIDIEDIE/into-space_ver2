using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricStructure : StructureBase
{
    private Vector3 playerLastPosition;
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            if (GameManager.Instance.GetSrc() >= GameManager.Instance.GetCurRemoveSrc())
            {
                playerLastPosition = GameManager.Instance.Player.transform.position;
                StartCoroutine(PerformAction());
            }
            else
            {
                Debug.Log("연료 부족!");
            }
        }
    }
    public override IEnumerator PerformAction()
    {
        isPerformingAction = true;
        Debug.Log("작업 시작!");

        float elapsedTime = 0f;


        while (elapsedTime < GameManager.Instance.GetproductElecTime())
        {
            if (!isNear)//플레이어가 감지 영역을 벗어난 경우
            {
                Debug.Log("작업 중단");
                isPerformingAction = false;
                yield break;
            }

            if (GameManager.Instance.GetSrc() < GameManager.Instance.GetCurRemoveSrc())//플레이어 작업중 연료부족
            {
                Debug.Log("연료부족!");
                isPerformingAction = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("작업 완료!");
        GameManager.Instance.AddElectric(GameManager.Instance.GetCurAddElectric());
        GameManager.Instance.ConsumeSrc(GameManager.Instance.GetCurRemoveSrc());

        isPerformingAction = false;

        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//작업반복
        }
    }
}
