using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerStructure : StructureBase
{
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            StartCoroutine(PerformAction());
        }
    }
    public override IEnumerator PerformAction() //������
    {
        isPerformingAction = true;
        GameManager.Instance.SetInteractionState(InteractionType.Steer, true);
        Debug.Log("���� ����!");

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            GameManager.Instance.onBoost();
            Debug.Log("���� ��");
            yield return new WaitForSeconds(0.5f);
        }
        GameManager.Instance.offBoost();
        GameManager.Instance.SetInteractionState(InteractionType.Src, false);
        Debug.Log("���� �ߴ�");
        isPerformingAction = false;
    }
}
