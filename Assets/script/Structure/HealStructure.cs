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
    public override IEnumerator PerformAction() //힐용
    {
        isPerformingAction = true;
        Debug.Log("치료 시작!");
        GameManager.Instance.SetInteractionState(InteractionType.Heal, true);
        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("치료 중");
            GameManager.Instance.AddHP(GameManager.Instance.GetCurAddHP());
            yield return new WaitForSeconds(GameManager.Instance.GetproductHealTime());
        }

        Debug.Log("치료 중단");
        GameManager.Instance.SetInteractionState(InteractionType.Heal, false);
        isPerformingAction = false;
    }
}
