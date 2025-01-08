using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SrcStructure : StructureBase
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
            StartCoroutine(PerformAction());
        }
    }
    public override IEnumerator PerformAction()
    {
        isPerformingAction = true;
        Debug.Log("�۾� ����!");
        GameManager.Instance.SetInteractionState(InteractionType.Src, true);
        float elapsedTime = 0f;

        progressImage_top.fillAmount = 0f;
        progressImage_bottom.gameObject.SetActive(true);
        progressImage_top.gameObject.SetActive(true);

        while ((elapsedTime < GameManager.Instance.GetproductSrcTime())&& (Input.GetKey(KeyCode.Space)))
        {
            if (!isNear)//�÷��̾ ���� ������ ��� ���
            {
                Debug.Log("�۾� �ߴ�");
                GameManager.Instance.SetInteractionState(InteractionType.Src, false);
                isPerformingAction = false;
                progressImage_bottom.gameObject.SetActive(false);
                progressImage_top.gameObject.SetActive(false);
                yield break;
            }

            elapsedTime += Time.deltaTime;
            progressImage_top.fillAmount = elapsedTime / GameManager.Instance.GetproductSrcTime();
            yield return null;
        }

        Debug.Log("�۾� �Ϸ�!");
        GameManager.Instance.AddSrc(GameManager.Instance.GetCurAddSrc());
        GameManager.Instance.SetInteractionState(InteractionType.Src, false);
        isPerformingAction = false;
        progressImage_bottom.gameObject.SetActive(false);
        progressImage_top.gameObject.SetActive(false);

        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//�۾��ݺ�
        }
    }
}

