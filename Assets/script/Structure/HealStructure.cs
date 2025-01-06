using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStructure : StructureBase
{
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            StartCoroutine(PerformAction());
        }
    }
    public override IEnumerator PerformAction() //����
    {
        isPerformingAction = true;
        Debug.Log("ġ�� ����!");
        GameManager.Instance.SetInteractionState(InteractionType.Heal, true);
        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("ġ�� ��");
            GameManager.Instance.AddHP(GameManager.Instance.GetCurAddHP());
            yield return new WaitForSeconds(GameManager.Instance.GetproductHealTime());
        }

        Debug.Log("ġ�� �ߴ�");
        GameManager.Instance.SetInteractionState(InteractionType.Heal, false);
        isPerformingAction = false;
    }
}
