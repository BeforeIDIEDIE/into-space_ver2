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
    public override IEnumerator PerformAction() //조종용
    {
        isPerformingAction = true;
        Debug.Log("조종 시작!");

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("조종 중");
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("조종 중단");
        isPerformingAction = false;
    }
}
