using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_action : MonoBehaviour
{
    private bool isNear;
    private bool isPerformingAction;
    float srcActionDuration;
    float elecActionDuration;
    [SerializeField] string structure;
    void Start()
    {
        isNear = false;
        isPerformingAction = false;
        srcActionDuration = 3f;
        elecActionDuration = 3f;
    }

    public void playerNeared() => isNear = true;
    public void playerOut() => isNear = false;
    private void Update()
    {
        switch(structure)
        {
            case "src":
                {
                    if (isNear && Input.GetKeyDown(KeyCode.Space) && !isPerformingAction)
                    {
                        StartCoroutine(srcPerformAction());
                    }
                    break;
                }
            case "electric":
                {
                    if (isNear && Input.GetKeyDown(KeyCode.Space) && !isPerformingAction)
                    {
                        StartCoroutine(electricPerformAction());
                    }
                    break;
                }
            case "heal":
                {
                    if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
                    {
                        StartCoroutine(healPerformAction());
                    }
                    break;
                }
            case "steer":
                {
                    if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
                    {
                        StartCoroutine(steerPerformAction());
                    }
                    break;
                }
            case "upgrade":
                {

                    //���� �ۼ� -> game manager����� ����
                    break;
                }
        }
    }
    private IEnumerator steerPerformAction() //������
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
    private IEnumerator healPerformAction() //����
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

    private IEnumerator electricPerformAction()//�����
    {
        isPerformingAction = true;
        Debug.Log("�۾� ����!");

        float elapsedTime = 0f;


        while (elapsedTime < elecActionDuration)
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
            StartCoroutine(electricPerformAction());//�۾��ݺ�
        }
    }

    private IEnumerator srcPerformAction()//�ҽ���
    {
        isPerformingAction = true;
        Debug.Log("�۾� ����!");

        float elapsedTime = 0f;
        

        while (elapsedTime < srcActionDuration)
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
            StartCoroutine(srcPerformAction());//�۾��ݺ�
        }
    }
}
