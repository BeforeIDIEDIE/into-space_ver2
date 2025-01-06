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

    //상호작용을 위한 열거형
    private Dictionary<InteractionType, bool> interactionStates;

    [SerializeField] public GameObject Player;

    //목표까지의 거리
    private float Dist = 0f;
    private float totalDist = 1260f;
    private float shipSpeed = 2f;
    private float shipConsume = 1f;
    private float activeSpeedMultiplier = 1.5f; 
    private float activeConsumeMultiplier = 2f; 
    private float previousDist;
    private bool isBoosted = false;

    //자원
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

    private float hp = 50f;
    private float maximumHP = 100f;
    private float curAddHP = 1f;
    private float previousHP;
    private float productHealTime = 0.5f;

    public float GetproductElecTime() => productElectricTime;
    public float GetproductSrcTime() => productSrcTime;
    public float GetproductHealTime() => productHealTime;


    //UI
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI srcText;
    [SerializeField] private TextMeshProUGUI electricText;
    [SerializeField] private TextMeshProUGUI shipdist;
    [SerializeField] private Slider distanceSlider;

    private void Start()
    {
        //초기 값 설정
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
        UpdateResourceUI();

        //슬라이더 용
        distanceSlider.minValue = 0;
        distanceSlider.maxValue = 1;
        distanceSlider.value = Dist/totalDist;
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
                Debug.Log($"현재거리 {Dist}, 연료 {src}");
            }
            else
            {
                Debug.Log("우주선이 멈춤");
            }

            yield return new WaitForSeconds(2f);
        }
        Debug.Log("목표 도달!");
        //아직 엔딩 안만듦!
    }

    // 조종석 상호작용
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


    //자원 추가
    public void AddSrc(float amount)
    {
        src += amount;
        Debug.Log($"Src 추가: {amount}, 현재 Src: {src}");
    }

    public void AddElectric(float amount)
    {
        electric += amount;
        Debug.Log($"Electric 추가: {amount}, 현재 Electric: {electric}");
    }

    public void AddHP(float amount)
    {
        hp = Mathf.Min(hp + amount, maximumHP);
        Debug.Log($"HP 추가: {amount}, 현재 HP: {hp}");
    }

    // 자원 소모
    public bool ConsumeSrc(float amount)
    {
        if (src >= amount)
        {
            src -= amount;
            Debug.Log($"Src 소모: {amount}\n현재 Src: {src}");
            return true;
        }
        else
        {
            Debug.Log("No Src!");
            return false;
        }
    }

    public bool ConsumeElectric(float amount)
    {
        if (electric >= amount)
        {
            electric -= amount;
            Debug.Log($"Electric 소모: {amount}\n현재 Electric: {electric}");
            return true;//성공적으로 소모
        }
        else
        {
            Debug.Log("No Electric!");
            return false;//소모 실패
        }
    }

    public bool ConsumeHP(float amount)
    {
        if (hp > 0)
        {
            hp -= amount;
            Debug.Log($"HP 소모: {amount}\n현재 HP: {hp}");
            if (hp <= 0)
            {
                Debug.Log("죽었다!!");
            }
            return true; // 성공적으로 소모
        }
        return false; // 체력이 이미 0
    }

    // 자원 상태 반환 함수
    public float GetSrc() => src;
    public float GetElectric() => electric;
    public float GetHP() => hp;
    public float GetCurAddSrc() => curAddSrc;
    public float GetCurAddHP() => curAddHP;
    public float GetCurAddElectric() => curAddElectric;
    public float GetCurRemoveSrc() => curRemoveSrc;
    public float GetBoostConsumeSrc() => shipConsume* activeConsumeMultiplier;


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
        return interactionStates.Values.Any(state => state);//하나라도 참이면 참
    }
}
