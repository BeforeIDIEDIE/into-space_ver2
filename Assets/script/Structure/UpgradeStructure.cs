using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStructure : StructureBase
{
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            //���׷��̵� UI����
        }
    }
    public override IEnumerator PerformAction() //�Ⱦ���!- > ��Ӷ����� ��¿�� ���� �ۼ�!
    {
        //����� ����!
        yield return new WaitForSeconds(0f);
    }
}
