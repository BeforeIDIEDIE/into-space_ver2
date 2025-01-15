using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private List<GameEvent> events; // �̺�Ʈ ������ ����
    private int currentEventIndex = 0; // ���� �̺�Ʈ �ε���
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
        // �̺�Ʈ �����͸� ���� �ʱ�ȭ
        events = new List<GameEvent>
        {
            new GameEvent//1
            {
                description = "��ǻ�Ϳ� �̻��� ���� 01011101 => (?)",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "181" },//���� x
                    new EventChoice { choiceText = "182" },//�Ϸ� ª����
                    new EventChoice { choiceText = "NULL" }//�Ϸ� ª����
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer{ answerText = "�ƹ� �̻� ����!"},
                    new EventAnswer{ answerText = "���� ���⸦ �Ҹ��ϴ� �ֱⰡ ��������..."},
                    new EventAnswer{ answerText = "���� ���⸦ �Ҹ��ϴ� �ֱⰡ ��������..."}
                }
            },
            new GameEvent//2
            {
                description = "�㰡 ħ���ߴ�! �������� �Ϳ�������?",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "�Ұ��Ѵ� " },//-> �̻� ����
                    new EventChoice { choiceText = "����д� " },//-> ���׷��̵� �Ұ�
                    new EventChoice { choiceText = "�Դ´� " }//-> ü�� 2 ȸ��
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "�㸦 �Ұ��ߴ�!" },
                    new EventAnswer { answerText = "�Ϸ絿�� ���׷��̵� �Ұ�..." },
                    new EventAnswer { answerText = "ü�� 2 ȸ��!" }
                }
            },
            new GameEvent//3
            {
                description = "�ֱ��� ���� ��뷮 �� �������� ��뷮�� �����ߴ�!",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "���� 20�� �Ҹ��Ͽ� ��ģ�� " },//-> �̻� ����
                    new EventChoice { choiceText = "����д� " },// �̻����
                    new EventChoice { choiceText = "�Ҹ��� ������Ų��. " }
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "�ƹ� �̻� ����!" },
                    new EventAnswer { answerText = "�ƹ� �̻� ����!" },
                    new EventAnswer { answerText = "�ֱ����� ����Ҹ� 5�����Ͽ���... ���ּ��� �ӵ��� ��������!" }
                }
            },
            new GameEvent//4
            {
                description = "���⸦ ������ �� ������� ���� �Ҹ��� �����ߴ�",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "�����Ѵ�." },//-> ����Ҹ�1����-> ���� ���� 1����
                    new EventChoice { choiceText = "����д� " },
                    new EventChoice { choiceText = "�Ҹ��� ������Ű�� ��?" }//�Ҹ� ���� -> ���� �Ҹ�2���� -> ���� ���� 2����.
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "���� ����� ���� �Ҹ��� 1���������� ���� ���귮�� 1 �����ߴ�...." },
                    new EventAnswer { answerText = "�ƹ� �̻� ����!" },
                    new EventAnswer { answerText = "���� ����� ���� �Ҹ��� 2���������� ���� ���귮�� 2�����Ѵ�!" }
                }
            },
            new GameEvent//5
            {
                description = "�ֱ��� ���� �Ҹ��� ������� �Ҹ� �߰��ߴ�",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "�̰� ������Ű�� ��� �ɱ�?" },//-> ġ�� ���� 2����, �ֱ� ����Ҹ� 10����
                    new EventChoice { choiceText = "����д� " },
                    new EventChoice { choiceText = "�����Ѵ�" }//�����Ѵ�. -> �ֱ��� ����Ҹ� 5����
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "�ֱ����� ���� �Ҹ��� 10����������.... ġ�� ���귮�� 2 �����ߴ�!" },
                    new EventAnswer { answerText = "�ƹ� �̻� ����!" },
                    new EventAnswer { answerText = "�ֱ����� ���� �Ҹ��� 5���ҵǾ���!" }
                }
            },
            new GameEvent//6
            {
                description = "�̷�! �������� ���� �߰��ߴ�.",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "���ش� " },//-> �̻� ����
                    new EventChoice { choiceText = "���� 10�� �Ҹ��Ͽ� ������ ����?" },//-> ���� 10 ����, �ӵ� 1����.
                    new EventChoice { choiceText = "����д� " }//ü�� ���ط��� 1 �����Ѵ�.
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "�ƹ� �̻� ����!" },
                    new EventAnswer { answerText = "���Ⱑ 10 ���ҵǾ�����... �ӵ��� 1 �����Ǿ���!" },
                    new EventAnswer { answerText = "ü�� ���ط��� �����Ѵ�...." }
                }
            },
            new GameEvent//7
            {
                description = "���濡 ��Ȧ 2���� �߰ߵǾ���?",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "���� ����" },//50%Ȯ���� 200�Ÿ� �߰�, 50%Ȯ���� 300�Ÿ� ���� 
                    new EventChoice { choiceText = "������ ����" },//50%Ȯ���� 400�Ÿ� �߰�, 50%Ȯ���� 600�Ÿ� ����
                    new EventChoice { choiceText = "���� x" }//������ ����.
                },
                answer = new List<EventAnswer>
                {
                    new EventAnswer { answerText = "�� �� ���� ������ �����Ͽ���!" },
                    new EventAnswer { answerText = "�� �� ���� ������ �����Ͽ���!" },
                    new EventAnswer { answerText = "���� ��� ����..." }
                }
            }
        };
    }

}
