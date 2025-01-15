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
        //        Debug.Log("���� ����! ������ �Ұ����մϴ�.");
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
    public override IEnumerator PerformAction() //������
    {
        isPerformingAction = true;
        GameManager.Instance.SetInteractionState(InteractionType.Steer, true);
        Debug.Log("���� ����!");

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            if (GameManager.Instance.GetSrc() < GameManager.Instance.GetCurRemoveSrc())
            {
                Debug.Log("���� �ߴ�.");
                GameManager.Instance.SetInteractionState(InteractionType.Steer, false);
                GameManager.Instance.offBoost();
                isPerformingAction = false;
                yield break;
            }

            GameManager.Instance.onBoost();
            Debug.Log("���� ��");
            yield return new WaitForSeconds(0.5f);
        }

        GameManager.Instance.offBoost();
        GameManager.Instance.SetInteractionState(InteractionType.Steer, false);
        Debug.Log("���� �ߴ�");
        isPerformingAction = false;
    }
}
