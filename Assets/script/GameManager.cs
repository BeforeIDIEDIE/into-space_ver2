using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

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

    //목표까지의 거리
    private float shipDist = 0f;
    private float totalDist = 0f;
    private float shipSpeed = 0f;
    private float shipConsume = 0f;

    //자원
    private float src = 0f;
    private float curAddSrc = 3f;
    private float maxSrc = 100f;
    private float curRemoveSrc = 2f;
    private float previousSrc;
    

    private float electric = 0f;
    private float curAddElectric = 3f;
    private float maxElectric = 100f;
    private float previousElectric;

    private float hp = 100f;
    private float maximumHP = 100f;
    private float curAddHP = 1f;
    private float previousHP;

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI srcText;
    [SerializeField] private TextMeshProUGUI electricText;

    private void Start()
    {
        // 초기 값 설정
        previousHP = hp;
        previousSrc = src;
        previousElectric = electric;

        // 초기 UI 업데이트
        UpdateResourceUI();
    }

    private void Update()
    {
        // 자원이 변경되었을 때만 UI 업데이트
        if (previousHP != hp || previousSrc != src || previousElectric != electric)
        {
            UpdateResourceUI();

            // 이전 값 갱신
            previousHP = hp;
            previousSrc = src;
            previousElectric = electric;
        }
    }

    private void UpdateResourceUI()
    {
        hpText.text = $"HP: {hp}/{maximumHP}";
        srcText.text = $"Src: {src}/{maxSrc}";
        electricText.text = $"Electric: {electric}/{maxElectric}";
    }


    public void shipMoving()
    {
        if(src<shipConsume)
        {
            Debug.Log("fail");
        }
        else
        {
            //우주선이 src소모
        }
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
}
