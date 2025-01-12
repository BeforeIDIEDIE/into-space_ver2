using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEditor.Build;
using System.Linq;

public enum InteractionType
{
    Heal,
    Electric,
    Src,
    Steer
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //�ð����
    [SerializeField] private Image dayProgressImage;//�Ϸ� ����� ǥ���� �̹���
    [SerializeField] private float dayDuration = 180f;
    private float currentTime = 0f;
    private int day = 1;

    //��ȣ�ۿ��� ���� ������
    private Dictionary<InteractionType, bool> interactionStates;
    
    [SerializeField] public GameObject Player;

    //��ǥ������ �Ÿ�
    private float Dist = 0f;
    private float totalDist = 2520f;
    private float shipSpeed = 2f;
    private float shipConsume = 1f;
    private float activeSpeedMultiplier = 1.5f; 
    private float activeConsumeMultiplier = 2f; 
    private float previousDist;
    private bool isBoosted = false;

    //�ڿ�
    private float src = 0f;
    private float curAddSrc = 6f;
    private float maxSrc = 100f;
    private float curRemoveSrc = 2f;
    private float previousSrc;
    private float productSrcTime = 3f;

    private float electric = 0f;
    private float curAddElectric = 3f;
    private float maxElectric = 100f;
    private float previousElectric;
    private float productElectricTime = 3f;
    private float defaultConsumeElec = 20f;

    private float hp = 50f;
    private float maximumHP = 100f;
    private float curAddHP = 1f;
    private float previousHP;
    private float productHealTime = 0.5f;

    private float reduceHpTime = 2f;
    private float reduceHpAmount = 1f;

    public float GetproductElecTime() => productElectricTime;
    public float GetproductSrcTime() => productSrcTime;
    public float GetproductHealTime() => productHealTime;


    //UI
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI srcText;
    [SerializeField] private TextMeshProUGUI electricText;
    [SerializeField] private TextMeshProUGUI shipdist;
    [SerializeField] private Slider distanceSlider;
    [SerializeField] private TextMeshProUGUI dayText;

    private void Start()
    {
        //�ʱ� �� ����
        interactionStates = new Dictionary<InteractionType, bool>
        {
            { InteractionType.Heal, false },
            { InteractionType.Electric, false },
            { InteractionType.Src, false },
            { InteractionType.Steer, false }
        };
        previousHP = hp;
        previousSrc = src;
        previousElectric = electric;
        previousDist = Dist;
        StartCoroutine(MoveShip());
        StartCoroutine(ReduceHpOverTime());
        UpdateResourceUI();

        //�����̴� ��
        distanceSlider.minValue = 0;
        distanceSlider.maxValue = 1;
        distanceSlider.value = Dist/totalDist;
        UpdateDayText();
    }

    private void Update()
    {
        if (previousHP != hp || previousSrc != src || previousElectric != electric || previousDist != Dist)
        {
            UpdateResourceUI();

            previousHP = hp;
            previousSrc = src;
            previousElectric = electric;
            previousDist = Dist;
        }
        UpdateDayProgress();
    }

    private void UpdateDayProgress()
    {
        if (currentTime < dayDuration)
        {
            currentTime += Time.deltaTime; 
            float progress = currentTime / dayDuration; 
            if (dayProgressImage != null)
            {
                dayProgressImage.fillAmount = progress; 
            }
        }
        else
        {
            currentTime = 0f;
            OnDayEnd();//�Ϸ� ���� ��� ������ �۾� ���� ����
            day++;
            UpdateDayText();
        }
    }
    private void UpdateDayText()
    {
        dayText.text = $"Day {day}";
    }

    private void OnDayEnd()
    {
        Debug.Log("�Ϸ� ��");
        ConsumeElectric(defaultConsumeElec+(day-1)*5);
    }

    private IEnumerator MoveShip()
    {
        while (Dist < totalDist)
        {
            float currentSpeed = isBoosted ? shipSpeed * activeSpeedMultiplier : shipSpeed;
            float currentConsume = isBoosted ? shipConsume * activeConsumeMultiplier : shipConsume;

            if (src >= currentConsume)
            {
                src -= currentConsume;
                Dist += currentSpeed;
                Debug.Log($"����Ÿ� {Dist}, ���� {src}");
            }
            else
            {
                Debug.Log("���ּ��� ����");
            }

            yield return new WaitForSeconds(2f);
        }
        Debug.Log("��ǥ ����!");
        //���� ���� �ȸ���!
    }

    // ������ ��ȣ�ۿ�
    public void onBoost()
    {
        isBoosted = true;
    }
    public void offBoost()
    {
        isBoosted = false;
    }

    private void UpdateResourceUI()
    {
        hpText.text = $"{hp}/{maximumHP}";
        srcText.text = $"{src}/{maxSrc}";
        electricText.text = $"{electric}/{maxElectric}";
        shipdist.text = $"Dist: {Dist}/{totalDist}";
        distanceSlider.value = Dist / totalDist;
    }


    //�ڿ� �߰�
    public void AddSrc(float amount)
    {
        src = Mathf.Min(src + amount, maxSrc);
        Debug.Log($"Src �߰�: {amount}, ���� Src: {src}");
    }

    public void AddDefaultElec(float amount)
    {
        if(amount>0)
        {
            defaultConsumeElec += amount;
        }
        else
        {
            defaultConsumeElec = Math.Max(defaultConsumeElec + amount, 0);
        }
    }

    public void AddElectric(float amount)
    {
        electric = Mathf.Min(electric + amount, maxElectric);
        Debug.Log($"Electric �߰�: {amount}, ���� Electric: {electric}");
    }

    public void AddHP(float amount)
    {
        hp = Mathf.Min(hp + amount, maximumHP);
        Debug.Log($"HP �߰�: {amount}, ���� HP: {hp}");
    }
    private IEnumerator ReduceHpOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(reduceHpTime); // 2�� ���
            if (hp > 0)
            {
                ConsumeHP(reduceHpAmount);
            }
        }
    }
    // �ڿ� �Ҹ�
    public bool ConsumeSrc(float amount)
    {
        if (src >= amount)
        {
            src -= amount;
            Debug.Log($"Src �Ҹ�: {amount}\n���� Src: {src}");
            return true;
        }
        else
        {
            Debug.Log("No Src!");
            return false;
        }
    }
    public bool ConsumeHP(float amount)
    {
        if (hp > 0)
        {
            hp -= amount;
            Debug.Log("���� HP: {hp}");
            if (hp <= 0)
            {
                Debug.Log("�׾���!!");
            }
            return true; 
        }
        return false;
    }
    public bool ConsumeElectric(float amount)
    {
        if (electric >= amount)
        {
            electric -= amount;
            Debug.Log($"Electric �Ҹ�: {amount}\n���� Electric: {electric}");
            return true;//���������� �Ҹ�
        }
        else
        {
            Debug.Log("No Electric!");
            return false;//�Ҹ� ����
        }
    }

    // �ڿ� ���� ��ȯ �Լ�
    public float GetSrc() => src;
    public void AddCurAddSrc( float amount)
    {
        curAddSrc += amount;
    }
    public float GetElectric() => electric;
    public void AddCurAddElectric(float amount)
    {
        curAddElectric += amount;
    }
    public float GetHP() => hp;
    public void AddCurAddHp(float amount)
    {
        curAddHP += amount;
    }
    public float GetCurAddSrc() => curAddSrc;
    public float GetCurAddHP() => curAddHP;
    public float GetCurAddElectric() => curAddElectric;
    public float GetCurRemoveSrc() => curRemoveSrc;
    public float GetBoostConsumeSrc() => shipConsume* activeConsumeMultiplier;
    public float GetMaxSrc() => maxSrc;
    public float GetMaxElec() => maxElectric;
    public float GetMaxHp() => maximumHP;


    public void SetInteractionState(InteractionType type, bool state)
    {
        interactionStates[type] = state;
    }

    public bool IsInteractionActive(InteractionType type)
    {
        return interactionStates[type];
    }

    public bool IsPlayerInteraction()
    {
        return interactionStates.Values.Any(state => state);//�ϳ��� ���̸� ��
    }
}
