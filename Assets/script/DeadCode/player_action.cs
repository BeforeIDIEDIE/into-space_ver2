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

                    //추후 작성 -> game manager만들고 나서
                    break;
                }
        }
    }
    private IEnumerator steerPerformAction() //조종용
    {
        isPerformingAction = true;
        Debug.Log("조종 시작!");

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("조종 중");
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("조종 중단");
        isPerformingAction = false;
    }
    private IEnumerator healPerformAction() //힐용
    {
        isPerformingAction = true;
        Debug.Log("치료 시작!");

        while (isNear && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("치료 중");
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("치료 중단");
        isPerformingAction = false;
    }

    private IEnumerator electricPerformAction()//전기용
    {
        isPerformingAction = true;
        Debug.Log("작업 시작!");

        float elapsedTime = 0f;


        while (elapsedTime < elecActionDuration)
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
            StartCoroutine(electricPerformAction());//작업반복
        }
    }

    private IEnumerator srcPerformAction()//소스용
    {
        isPerformingAction = true;
        Debug.Log("작업 시작!");

        float elapsedTime = 0f;
        

        while (elapsedTime < srcActionDuration)
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
            StartCoroutine(srcPerformAction());//작업반복
        }
    }
}
