using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerStructure : StructureBase
{
    private void Update()
    {
        //if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        //{
        //    if (GameManager.Instance.GetSrc() >= GameManager.Instance.GetCurRemoveSrc())
        //    {
        //        StartCoroutine(PerformAction());
        //    }
        //    else
        //    {
        //        Debug.Log("연료 부족! 조종이 불가능합니다.");
        //    }
        //}
        if (isNear && Input.GetKey(KeyCode.Space))
        {
            GameManager.Instance.SetInteractionState(InteractionType.Steer, true);
            GameManager.Instance.onBoost();
        }
        else
        {
            GameManager.Instance.offBoost();
            GameManager.Instance.SetInteractionState(InteractionType.Steer, false);
        }
    }
    public override IEnumerator PerformAction() //조종용
    {
        isPerformingAction = true;
        GameManager.Instance.SetInteractionState(InteractionType.Steer, true);
        Debug.Log("조종 시작!");

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            if (GameManager.Instance.GetSrc() < GameManager.Instance.GetCurRemoveSrc())
            {
                Debug.Log("조종 중단.");
                GameManager.Instance.SetInteractionState(InteractionType.Steer, false);
                GameManager.Instance.offBoost();
                isPerformingAction = false;
                yield break;
            }

            GameManager.Instance.onBoost();
            Debug.Log("조종 중");
            yield return new WaitForSeconds(0.5f);
        }

        GameManager.Instance.offBoost();
        GameManager.Instance.SetInteractionState(InteractionType.Steer, false);
        Debug.Log("조종 중단");
        isPerformingAction = false;
    }
}
