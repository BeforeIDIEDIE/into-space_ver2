using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElectricStructure : StructureBase
{

    [SerializeField] private Image progressImage_bottom;
    [SerializeField] private Image progressImage_top;

    private void Start()
    {
        progressImage_bottom.gameObject.SetActive(false);
        progressImage_top.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            if (GameManager.Instance.GetSrc() >= GameManager.Instance.GetCurRemoveSrc())
            {
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
        GameManager.Instance.SetInteractionState(InteractionType.Electric, true);
        float elapsedTime = 0f;

        progressImage_top.fillAmount = 0f;
        progressImage_bottom.gameObject.SetActive(true);
        progressImage_top.gameObject.SetActive(true);

        while ((elapsedTime < GameManager.Instance.GetproductElecTime())&& Input.GetKey(KeyCode.Space))
        {
            if (!isNear)//�÷��̾ ���� ������ ��� ���
            {
                Debug.Log("�۾� �ߴ�");
                GameManager.Instance.SetInteractionState(InteractionType.Electric, false);
                isPerformingAction = false;
                progressImage_bottom.gameObject.SetActive(false);
                progressImage_top.gameObject.SetActive(false);
                yield break;
            }

            if (GameManager.Instance.GetSrc() < GameManager.Instance.GetCurRemoveSrc())//�÷��̾� �۾��� �������
            {
                Debug.Log("�������!");
                GameManager.Instance.SetInteractionState(InteractionType.Electric, false);
                isPerformingAction = false;
                progressImage_bottom.gameObject.SetActive(false);
                progressImage_top.gameObject.SetActive(false);
                yield break;
            }
            progressImage_top.fillAmount = elapsedTime / GameManager.Instance.GetproductSrcTime();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("�۾� �Ϸ�!");
        GameManager.Instance.SetInteractionState(InteractionType.Electric, false);
        GameManager.Instance.AddElectric(GameManager.Instance.GetCurAddElectric());
        GameManager.Instance.ConsumeSrc(GameManager.Instance.GetCurRemoveSrc());

        isPerformingAction = false;
        progressImage_bottom.gameObject.SetActive(false);
        progressImage_top.gameObject.SetActive(false);
        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//�۾��ݺ�
        }
    }
}
