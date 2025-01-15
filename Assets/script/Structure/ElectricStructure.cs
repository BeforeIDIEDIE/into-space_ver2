using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElectricStructure : StructureBase
{

    [SerializeField] private Image progressImage_bottom;
    [SerializeField] private Image progressImage_top;

    [SerializeField] private Image progress_all;

    private void Start()
    {
        progressImage_bottom.gameObject.SetActive(false);
        progressImage_top.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction&&!GameManager.Instance.IsGameOver())
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
        progress_all.fillAmount = GameManager.Instance.GetElectric()/GameManager.Instance.GetMaxElec();
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

            if (GameManager.Instance.GetSrc() < GameManager.Instance.GetCurRemoveSrc() && !GameManager.Instance.IsGameOver())//플레이어 작업중 연료부족+ 게임 오버시
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
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction && !GameManager.Instance.IsGameOver())
        {
            StartCoroutine(PerformAction());//작업반복
        }
    }
}
