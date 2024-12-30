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
        //플레이어가 근접한 상태에서 스페이스 키 입력
        if (structure == "src"&&isNear && Input.GetKeyDown(KeyCode.Space) && !isPerformingAction)
        {
            StartCoroutine(PerformAction());
        }
    }

    private IEnumerator PerformAction()
    {
        isPerformingAction = true;
        Debug.Log("작업 시작!");

        float elapsedTime = 0f;
        

        while (elapsedTime < actionDuration)
        {
            if (!isNear)//플레이어가 감지 영역을 벗어난 경우
            {
                Debug.Log("작업 중단");
                isPerformingAction = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("작업 완료!");
        //AddResource(); // -> 추후 추가할 것

        isPerformingAction = false;

        if (isNear && Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(PerformAction());//작업반복
        }
    }
}
