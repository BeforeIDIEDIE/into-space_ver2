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

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("ġ�� ��");
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("ġ�� �ߴ�");
        isPerformingAction = false;
    }
}
