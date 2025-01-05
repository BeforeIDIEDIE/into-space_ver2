using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricStructure : StructureBase
{
    private Vector3 playerLastPosition;
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            if (GameManager.Instance.GetSrc() >= GameManager.Instance.GetCurRemoveSrc())
            {
                playerLastPosition = GameManager.Instance.Player.transform.position;
                StartCoroutine(PerformAction());
            }
            else
            {
                Debug.Log("���� ����!");
            }
        }
    }
    public override IEnumerator PerformAction()
    {
        isPerformingAction = true;
        Debug.Log("�۾� ����!");

        float elapsedTime = 0f;


        while (elapsedTime < GameManager.Instance.GetproductElecTime())
        {
            if (!isNear)//�÷��̾ ���� ������ ��� ���
            {
                Debug.Log("�۾� �ߴ�");
                isPerformingAction = false;
                yield break;
            }

            if (GameManager.Instance.GetSrc() < GameManager.Instance.GetCurRemoveSrc())//�÷��̾� �۾��� �������
            {
                Debug.Log("�������!");
                isPerformingAction = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("�۾� �Ϸ�!");
        GameManager.Instance.AddElectric(GameManager.Instance.GetCurAddElectric());
        GameManager.Instance.ConsumeSrc(GameManager.Instance.GetCurRemoveSrc());

        isPerformingAction = false;

        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//�۾��ݺ�
        }
    }
}
