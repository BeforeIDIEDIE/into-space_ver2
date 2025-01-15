using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealStructure : StructureBase
{

    [SerializeField] private Image progressImage_top;

    private void Start()
    {
        progressImage_top.gameObject.SetActive(false);
    }

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
        progressImage_top.gameObject.SetActive(true);
        progressImage_top.fillAmount = 1f;
        Debug.Log("ġ�� ����!");
        GameManager.Instance.SetInteractionState(InteractionType.Heal, true);
        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("ġ�� ��");
            GameManager.Instance.AddHP(GameManager.Instance.GetCurAddHP());
            yield return new WaitForSeconds(GameManager.Instance.GetproductHealTime());
        }

        Debug.Log("ġ�� �ߴ�");
        progressImage_top.gameObject.SetActive(false);
        GameManager.Instance.SetInteractionState(InteractionType.Heal, false);
        isPerformingAction = false;
    }
}
