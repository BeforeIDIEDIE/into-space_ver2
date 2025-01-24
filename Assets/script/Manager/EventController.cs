using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    private int selectedChoiceIndex = -1;
    private List<GameEvent> events; // 이벤트 데이터 저장
    [SerializeField] private TextMeshProUGUI problemAndAnswer;
    [SerializeField] private TextMeshProUGUI sel1;
    [SerializeField] private TextMeshProUGUI sel2;
    [SerializeField] private TextMeshProUGUI sel3;
    [SerializeField] private float textDisplaySpeed = 0.1f;
    [SerializeField] private Image curImg;
    [SerializeField] private List<Sprite> eventSprites;
    [SerializeField] private Button sel1BTN;
    [SerializeField] private Button sel2BTN;
    [SerializeField] private Button sel3BTN;
    [SerializeField] private Button Enter;
    [SerializeField] private GameObject SRCUI;
    [SerializeField] private GameObject eventUI;
    [SerializeField] private speedController controller;

    private List<bool> isSelected = new List<bool>();

    //버튼 상태
    private void SetButtonState(bool sel1State, bool sel2State, bool sel3State, bool enterState)
    {
        sel1BTN.interactable = sel1State;
        sel2BTN.interactable = sel2State;
        sel3BTN.interactable = sel3State;
        Enter.interactable = enterState;
    }
    private void allBTNDisable() => SetButtonState(false, false, false, false);
    private void OnlyEnterBTNAble() => SetButtonState(false, false, false, true);
    private void selChoice() => SetButtonState(true, true, true, false);

    private void Start()
    {
        InitializeEvents();
        InitializeSelectionFlags();
    }
    private void InitializeEvents()
    {
        // 이벤트 데이터를 직접 초기화
        events = new List<GameEvent>
        {
            new GameEvent//1
            {
                description = "컴퓨터에 이상이 발생하였다! 01011101 => (?)",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "181" },//문제 x
                    new EventChoice { choiceText = "182" },//하루 짧아짐
                    new EventChoice { choiceText = "NULL" }//하루 짧아짐
                },
                answers = new List<EventAnswer>
                {
                    new EventAnswer{ answerText = "아무 이상 없다!"},
                    new EventAnswer{ answerText = "이제 전기를 소모하는 주기가 빨라진다..."},
                    new EventAnswer{ answerText = "이제 전기를 소모하는 주기가 빨라진다..."}
                },
                eventSprite = eventSprites[0]
            },
            new GameEvent//2
            {
                description = "쥐가 침입했다! 생각보다 귀여울지도?",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "소각한다 " },//-> 이상 없음
                    new EventChoice { choiceText = "내비둔다 " },//-> 업그레이드 불가
                    new EventChoice { choiceText = "먹는다 " }//-> 체력 2 회복
                },
                answers = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "쥐를 소각했다!\n아무 이상 없다!" },
                    new EventAnswer { answerText = "하루동안 업그레이드 불가..." },
                    new EventAnswer { answerText = "체력 2 회복!" }
                },
                eventSprite = eventSprites[1]
            },
            new GameEvent//3
            {
                description = $"주기적 전기 사용량 중 쓸데없는 사용량을 감지했다!\n현재 보유 전기 : {GameManager.Instance.GetElectric()}",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "전기 20을 소모하여 고친다 " },//-> 이상 없음
                    new EventChoice { choiceText = "내비둔다 " },// 이상없음
                    new EventChoice { choiceText = "소모량을 증가시킨다. " }
                },
                answers = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "주기적인 전기소모가 5증가하였다... 우주선의 속도가 빨라진다!" }
                },
                eventSprite = eventSprites[2]
            },
            new GameEvent//4
            {
                description = "전기를 생산할 때 쓸모없는 연료 소모량을 감지했다",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "개선한다." },//-> 연료소모량1감소-> 전기 생산 1감소
                    new EventChoice { choiceText = "내비둔다 " },
                    new EventChoice { choiceText = "소모량을 증가시키는 건?" }//소모량 증가 -> 연료 소모량2증가 -> 전기 생산 2증가.
                },
                answers = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "전기 생산시 연료 소모량이 1감소하지만 \n전기 생산량도 1 감소했다...." },
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "전기 생산시 연료 소모량이 2증가하지만 전기 생산량이 3증가한다!" }
                },
                eventSprite = eventSprites[3]
            },
            new GameEvent//5
            {
                description = "주기적 전기 소모량중 쓸모없는 소모를 발견했다",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "이걸 증가시키면 어떻게 될까?" },//-> 치료 생산 2증가, 주기 전기소모 10증가
                    new EventChoice { choiceText = "내비둔다 " },
                    new EventChoice { choiceText = "개선한다" }//개선한다. -> 주기적 전기소모 5감소
                },
                answers = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "주기적인 전기 소모량이 10증가하지만.... 치료 생산량이 2 증가했다!" },
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "주기적인 전기 소모량이 5감소되었다!" }
                },
                eventSprite = eventSprites[4]
            },
            new GameEvent//6
            {
                description = $"이런! 몸속에서 돌연변이 인자를 발견했다.\n현재 보유 전기 : {GameManager.Instance.GetElectric()}",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "없앤다 " },//-> 이상 없음
                    new EventChoice { choiceText = "전기 10을 소모하여 연구해 볼까?" },//-> 전기 10 감소, 속도 1증가.
                    new EventChoice { choiceText = "내비둔다 " }//체력 피해량이 1 증가한다.
                },
                answers = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "전기가 10 감소되었으나... 속도가 1 증가되었다!" },
                    new EventAnswer { answerText = "체력 피해량이 증가한다...." }
                },
                eventSprite = eventSprites[5]
            },
            new GameEvent//7
            {
                description = "전방에 웜홀 2개가 발견되었다?",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "왼쪽 진입" },//50%확률로 200거리 추가, 50%확률로 300거리 감소 
                    new EventChoice { choiceText = "오른쪽 진입" },//50%확률로 300거리 추가, 50%확률로 350거리 감소
                    new EventChoice { choiceText = "진입 하지 않는다." }//가던길 간다.
                },
                answers = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "알 수 없는 곳으로 도착하였다!" },
                    new EventAnswer { answerText = "알 수 없는 곳으로 도착하였다!" },
                    new EventAnswer { answerText = "가던 길로 가자..." }
                },
                eventSprite = eventSprites[6]
            }
        };
    }
    private void InitializeSelectionFlags()
    {
        isSelected = new List<bool>();
        for (int i = 0; i < events.Count; i++)
        {
            isSelected.Add(false);
        }
    }

    private int GetRandomEventIndex()
    {
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < isSelected.Count; i++)
        {
            if (!isSelected[i])
            {
                availableIndices.Add(i);
            }
        }

        // 모든 이벤트가 선택되었으면 플래그 초기화
        if (availableIndices.Count == 0)
        {
            ResetSelectionFlags();
            for (int i = 0; i < isSelected.Count; i++)
            {
                availableIndices.Add(i);
            }
        }
        int randomIndex = availableIndices[Random.Range(0, availableIndices.Count)];
        isSelected[randomIndex] = true;
        return randomIndex;
    }

    private void ResetSelectionFlags()
    {
        for (int i = 0; i < isSelected.Count; i++)
        {
            isSelected[i] = false;
        }
        Debug.Log("모든 이벤트 선택됨");
    }
    private void OnChoiceSelected(int eventIDX, int selIDX)
    {
        selectedChoiceIndex = selIDX; 
        GameEvent currentEvent = events[eventIDX];
        StartCoroutine(TypeAnswerText(currentEvent.answers[selIDX].answerText));
    }

    public void ActivePrintProblem()
    {
        printProblem();
    }
    private void printProblem()
    {
        SRCUI.SetActive(false);
        Time.timeScale = 0f;
        eventUI.SetActive(true);
        int curIdx = GetRandomEventIndex();
        StartCoroutine(DisplayEvent(curIdx));
        sel1BTN.onClick.RemoveAllListeners();
        sel1BTN.onClick.AddListener(() => OnChoiceSelected(curIdx, 0));

        sel2BTN.onClick.RemoveAllListeners();
        sel2BTN.onClick.AddListener(() => OnChoiceSelected(curIdx, 1));

        sel3BTN.onClick.RemoveAllListeners();
        sel3BTN.onClick.AddListener(() => OnChoiceSelected(curIdx, 2));
        //Enter버튼이 눌리면 startEventAnswer함수 실행
        Enter.onClick.RemoveAllListeners();
        Enter.onClick.AddListener(() => OnEnterPressed(curIdx));
        GameManager.Instance.UpdateConsumeElectricText();
    }

    private void OnEnterPressed(int eventIDX)
    {
        Time.timeScale = controller.GetIsTwo() ? 2.0f : 1.0f;
        eventUI.SetActive(false);
        SRCUI.SetActive(true);
        startEventAnswer(eventIDX, selectedChoiceIndex);
        selectedChoiceIndex = -1;
    }

    private IEnumerator DisplayEvent(int idxOfEvent)
    {
        problemAndAnswer.text = "";
        sel1.text = "";
        sel2.text = "";
        sel3.text = "";
        curImg.sprite = null;

        allBTNDisable();

        GameEvent randomEvent = events[idxOfEvent];
        curImg.sprite = randomEvent.eventSprite;

        foreach (char c in randomEvent.description)
        {
            problemAndAnswer.text += c;
            yield return new WaitForSecondsRealtime(textDisplaySpeed); // 변경
        }

        yield return new WaitForSecondsRealtime(0.3f); // 변경

        List<TextMeshProUGUI> selectionTexts = new List<TextMeshProUGUI> { sel1, sel2, sel3 };
        for (int i = 0; i < randomEvent.choices.Count; i++)
        {
            string choiceTextWithNumber = $"{i + 1}. {randomEvent.choices[i].choiceText}";
            foreach (char c in choiceTextWithNumber)
            {
                selectionTexts[i].text += c;
                yield return new WaitForSecondsRealtime(textDisplaySpeed); // 변경
            }
        }
        yield return new WaitForSecondsRealtime(0.3f); // 변경
        selChoice();
    }

    private IEnumerator TypeAnswerText(string answerText)
    {
        allBTNDisable();
        problemAndAnswer.text = "";
        sel1.text = "";
        sel2.text = "";
        sel3.text = "";
        foreach (char c in answerText)
        {
            problemAndAnswer.text += c;
            yield return new WaitForSecondsRealtime(textDisplaySpeed); // 변경
        }
        OnlyEnterBTNAble();
    }
    private void startEventAnswer(int eventIDX, int answerIDX)
    {
        switch (3 * eventIDX + answerIDX)
        {
            case 0:
                {
                    Debug.Log("문제없다.");
                    break;
                }
            case 1:
                {
                    GameManager.Instance.reduceDayDuration(30f);
                    break;
                }
            case 2:
                {
                    GameManager.Instance.reduceDayDuration(30f);
                    break;
                }
            case 3:
                {
                    Debug.Log("문제없다.");
                    break;
                }
            case 4:
                {
                    GameManager.Instance.OffUpgrade();
                    break;
                }
            case 5:
                {
                    GameManager.Instance.AddHP(2);
                    break;
                }
            case 6:
                {
                    GameManager.Instance.ConsumeElectric(20);
                    break;
                }
            case 7:
                {
                    Debug.Log("문제없다.");
                    break;
                }
            case 8:
                {
                    GameManager.Instance.AddDefaultConsumeElec(5);
                    GameManager.Instance.AddshipSpeed(2);
                    break;
                }
            case 9:
                {
                    GameManager.Instance.RemoveCurRemoveSrc(1);
                    GameManager.Instance.RemoveCurAddElectric(1);
                    break;
                }
            case 10:
                {
                    Debug.Log("문제없다.");
                    break;
                }
            case 11:
                {
                    GameManager.Instance.AddCurRemovedSrc(2);
                    GameManager.Instance.AddCurAddElectric(3);
                    break;
                }
            case 12:
                {
                    GameManager.Instance.AddDefaultConsumeElec(10);
                    GameManager.Instance.AddCurAddHp(2);
                    break;
                }
            case 13:
                {
                    Debug.Log("문제없다.");
                    break;
                }
            case 14:
                {
                    GameManager.Instance.ReduceDefaultConsumeElec(5);
                    break;
                }
            case 15:
                {
                    Debug.Log("문제없다.");
                    break;
                }
            case 16:
                {
                    GameManager.Instance.ConsumeElectric(10);
                    GameManager.Instance.AddPlayerSpeed(1);
                    break;
                }
            case 17:
                {
                    GameManager.Instance.AddReduceHPAmount(1);
                    break;
                }
            case 18:
                {
                    float rand = Random.Range(0, 2);
                    switch(rand)
                    {
                        case 0:
                            {
                                GameManager.Instance.AddDist(200);
                                break;
                            }
                        case 1:
                            {
                                GameManager.Instance.AddDist(300);
                                break;
                            }
                    }
                    break;
                }
            case 19:
                {
                    float rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0:
                            {
                                GameManager.Instance.AddDist(300);
                                break;
                            }
                        case 1:
                            {
                                GameManager.Instance.AddDist(350);
                                break;
                            }
                    }
                    break;
                }
            case 20:
                {
                    Debug.Log("문제없다.");
                    break;
                }
        }
    }
}
