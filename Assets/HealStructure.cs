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

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("치료 중");
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("치료 중단");
        isPerformingAction = false;
    }
}
