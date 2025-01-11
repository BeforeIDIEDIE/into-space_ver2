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
                Debug.Log("연료 부족!");
            }
        }
    }
    public override IEnumerator PerformAction()
    {
        isPerformingAction = true;
        Debug.Log("작업 시작!");
        GameManager.Instance.SetInteractionState(InteractionType.Electric, true);
        float elapsedTime = 0f;

        progressImage_top.fillAmount = 0f;
        progressImage_bottom.gameObject.SetActive(true);
        progressImage_top.gameObject.SetActive(true);

        while ((elapsedTime < GameManager.Instance.GetproductElecTime())&& Input.GetKey(KeyCode.Space))
        {
            if (!isNear)//플레이어가 감지 영역을 벗어난 경우
            {
                Debug.Log("작업 중단");
                GameManager.Instance.SetInteractionState(InteractionType.Electric, false);
                isPerformingAction = false;
                progressImage_bottom.gameObject.SetActive(false);
                progressImage_top.gameObject.SetActive(false);
                yield break;
            }

            if (GameManager.Instance.GetSrc() < GameManager.Instance.GetCurRemoveSrc())//플레이어 작업중 연료부족
            {
                Debug.Log("연료부족!");
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

        Debug.Log("작업 완료!");
        GameManager.Instance.SetInteractionState(InteractionType.Electric, false);
        GameManager.Instance.AddElectric(GameManager.Instance.GetCurAddElectric());
        GameManager.Instance.ConsumeSrc(GameManager.Instance.GetCurRemoveSrc());

        isPerformingAction = false;
        progressImage_bottom.gameObject.SetActive(false);
        progressImage_top.gameObject.SetActive(false);
        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//작업반복
        }
    }
}
