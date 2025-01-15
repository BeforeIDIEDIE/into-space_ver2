using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventController : MonoBehaviour
{
    private List<GameEvent> events; // �̺�Ʈ ������ ����
    private int currentEventIndex = 0; // ���� �̺�Ʈ �ε���

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
                }
            },
            new GameEvent//2
            {
                description = "�㰡 ħ���ߴ�! �������� �Ϳ�������?",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "��´� " },//-> �̻� ����
                    new EventChoice { choiceText = "����д� " },//-> ���׷��̵� �Ұ�
                    new EventChoice { choiceText = "�Դ´� " }//-> ü�� 2 ȸ��
                }
            },
            new GameEvent//3
            {
                description = "�ֱ��� ���� ��뷮 �� �������� ��뷮�� �����ߴ�!",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "���� 20�� �Ҹ��Ͽ� ��ģ�� " },//-> �̻� ����
                    new EventChoice { choiceText = "����д� " },// �̻����
                    new EventChoice { choiceText = "�Ҹ��� ������Ų��. " }//���� ���� 30�� ������� �ֱ��� ���� �Ҹ� 5����
                }
            },
            new GameEvent//4
            {
                description = "���⸦ ������ �� ������� ���� �Ҹ��� �����ߴ�",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "�����Ѵ�." },//-> ����Ҹ�1����-> ���� ���� 1����
                    new EventChoice { choiceText = "����д� " },//-> ���׷��̵� �Ұ�
                    new EventChoice { choiceText = "�Ҹ��� ������Ű�� ��?" }//�Ҹ� ���� -> ���� �Ҹ�2���� -> ���� ���� 2����.
                }
            },
            new GameEvent//5
            {
                description = "�ֱ��� ���� �Ҹ��� ������� �Ҹ� �߰��ߴ�",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "�̰� ������Ű�� ��� �ɱ�?" },//-> ġ�� ���� 2����, �ֱ� ����Ҹ� 10����
                    new EventChoice { choiceText = "����д� " },//-> ���׷��̵� �Ұ�
                    new EventChoice { choiceText = "�����Ѵ�" }//�����Ѵ�. -> �ֱ��� ����Ҹ� 5����
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
                }
            },
            new GameEvent//7
            {
                description = "���濡 ��Ȧ 2���� �߰ߵǾ���?",
                choices = new List<EventChoice>
                {
                    new EventChoice { choiceText = "���� ����" },//50%Ȯ���� 200�Ÿ� �߰�, 50%Ȯ���� 300�Ÿ� ���� 
                    new EventChoice { choiceText = "������ ����" },//50%Ȯ���� 200�Ÿ� �߰�, 50%Ȯ���� 300�Ÿ� ����
                    new EventChoice { choiceText = "���� x" }//������ ����.
                }
            }
        };
    }
}
