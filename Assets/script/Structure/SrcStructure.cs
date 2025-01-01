using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SrcStructure : StructureBase
{
    private float actionDuration = 3f;
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            StartCoroutine(PerformAction());
        }
    }
    public override IEnumerator PerformAction()
    {
        isPerformingAction = true;
        Debug.Log("�۾� ����!");

        float elapsedTime = 0f;


        while (elapsedTime < actionDuration)
        {
            if (!isNear)//�÷��̾ ���� ������ ��� ���
            {
                Debug.Log("�۾� �ߴ�");
                isPerformingAction = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("�۾� �Ϸ�!");
        GameManager.Instance.AddSrc(GameManager.Instance.GetCurAddSrc());

        isPerformingAction = false;

        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//�۾��ݺ�
        }
    }
}

