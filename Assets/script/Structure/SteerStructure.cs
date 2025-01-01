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
        Debug.Log("���� ����!");

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("���� ��");
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("���� �ߴ�");
        isPerformingAction = false;
    }
}
