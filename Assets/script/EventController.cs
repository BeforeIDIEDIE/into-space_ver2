using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private List<GameEvent> events; // 이벤트 데이터 저장
    private int currentEventIndex = 0; // 현재 이벤트 인덱스
    [SerializeField] private TextMeshProUGUI problem;
    [SerializeField] private TextMeshProUGUI sel1;
    [SerializeField] private TextMeshProUGUI sel2;
    [SerializeField] private TextMeshProUGUI sel3;
    private void Start()
    {
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        // 이벤트 데이터를 직접 초기화
        events = new List<GameEvent>
        {
            new GameEvent//1
            {
                description = "컴퓨터에 이상이 생김 01011101 => (?)",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "181" },//문제 x
                    new EventChoice { choiceText = "182" },//하루 짧아짐
                    new EventChoice { choiceText = "NULL" }//하루 짧아짐
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer{ answerText = "아무 이상 없다!"},
                    new EventAnswer{ answerText = "이제 전기를 소모하는 주기가 빨라진다..."},
                    new EventAnswer{ answerText = "이제 전기를 소모하는 주기가 빨라진다..."}
                }
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
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "쥐를 소각했다!" },
                    new EventAnswer { answerText = "하루동안 업그레이드 불가..." },
                    new EventAnswer { answerText = "체력 2 회복!" }
                }
            },
            new GameEvent//3
            {
                description = "주기적 전기 사용량 중 쓸데없는 사용량을 감지했다!",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "전기 20을 소모하여 고친다 " },//-> 이상 없음
                    new EventChoice { choiceText = "내비둔다 " },// 이상없음
                    new EventChoice { choiceText = "소모량을 증가시킨다. " }
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "주기적인 전기소모가 5증가하였다... 우주선의 속도가 빨라진다!" }
                }
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
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "전기 생산시 연료 소모량이 1감소하지만 전기 생산량도 1 감소했다...." },
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "전기 생산시 연료 소모량이 2증가하지만 전기 생산량이 2증가한다!" }
                }
            },
            new GameEvent//5
            {
                description = "주기적 전기 소모량중 쓸모없는 소모를 발견했다",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "이거 증가시키면 어떻게 될까?" },//-> 치료 생산 2증가, 주기 전기소모 10증가
                    new EventChoice { choiceText = "내비둔다 " },
                    new EventChoice { choiceText = "개선한다" }//개선한다. -> 주기적 전기소모 5감소
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "주기적인 전기 소모량이 10증가하지만.... 치료 생산량이 2 증가했다!" },
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "주기적인 전기 소모량이 5감소되었다!" }
                }
            },
            new GameEvent//6
            {
                description = "이런! 돌연변이 인자 발견했다.",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "없앤다 " },//-> 이상 없음
                    new EventChoice { choiceText = "전기 10을 소모하여 연구해 볼까?" },//-> 전기 10 감소, 속도 1증가.
                    new EventChoice { choiceText = "내비둔다 " }//체력 피해량이 1 증가한다.
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "아무 이상 없다!" },
                    new EventAnswer { answerText = "전기가 10 감소되었으나... 속도가 1 증가되었다!" },
                    new EventAnswer { answerText = "체력 피해량이 증가한다...." }
                }
            },
            new GameEvent//7
            {
                description = "전방에 웜홀 2개가 발견되었다?",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "왼쪽 진입" },//50%확률로 200거리 추가, 50%확률로 300거리 감소 
                    new EventChoice { choiceText = "오른쪽 진입" },//50%확률로 400거리 추가, 50%확률로 600거리 감소
                    new EventChoice { choiceText = "진입 x" }//가던길 간다.
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "알 수 없는 곳으로 도착하였다!" },
                    new EventAnswer { answerText = "알 수 없는 곳으로 도착하였다!" },
                    new EventAnswer { answerText = "가던 길로 가자..." }
                }
            }
        };
    }

}
