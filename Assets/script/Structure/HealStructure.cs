using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealStructure : StructureBase
{

    [SerializeField] private Image progressImage_top;
    [SerializeField] private Image progress_all;
    private void Start()
    {
        progressImage_top.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction && !isPerformingAction && !GameManager.Instance.IsGameOver())
        {
            StartCoroutine(PerformAction());
        }
        progress_all.fillAmount = GameManager.Instance.GetHP() / GameManager.Instance.GetMaxHp();
    }
    public override IEnumerator PerformAction() //힐용
    {
        isPerformingAction = true;
        progressImage_top.gameObject.SetActive(true);
        progressImage_top.fillAmount = 1f;
        Debug.Log("치료 시작!");
        GameManager.Instance.SetInteractionState(InteractionType.Heal, true);
        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("치료 중");
            GameManager.Instance.AddHP(GameManager.Instance.GetCurAddHP());
            yield return new WaitForSeconds(GameManager.Instance.GetproductHealTime());
        }

        Debug.Log("치료 중단");
        progressImage_top.gameObject.SetActive(false);
        GameManager.Instance.SetInteractionState(InteractionType.Heal, false);
        isPerformingAction = false;
    }
}
