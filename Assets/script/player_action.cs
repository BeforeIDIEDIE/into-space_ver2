using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_action : MonoBehaviour
{
    private bool isNear;
    private bool isPerformingAction;
    float actionDuration;
    [SerializeField] string structure;
    void Start()
    {
        isNear = false;
        isPerformingAction = false;
        actionDuration = 3f;
    }

    public void playerNeared()
    {
        isNear = true;
    }

    public void playerOut()
    {
        isNear = false;
    }
    private void Update()
    {
        //�÷��̾ ������ ���¿��� �����̽� Ű �Է�
        if (structure == "src"&&isNear && Input.GetKeyDown(KeyCode.Space) && !isPerformingAction)
        {
            StartCoroutine(PerformAction());
        }
    }

    private IEnumerator PerformAction()
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
        //AddResource(); // -> ���� �߰��� ��

        isPerformingAction = false;

        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//�۾��ݺ�
        }
    }
}
