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

    //��ǥ������ �Ÿ�
    private float shipDist = 0f;
    private float totalDist = 0f;
    private float shipSpeed = 0f;
    private float shipConsume = 0f;

    //�ڿ�
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
        // �ʱ� �� ����
        previousHP = hp;
        previousSrc = src;
        previousElectric = electric;

        // �ʱ� UI ������Ʈ
        UpdateResourceUI();
    }

    private void Update()
    {
        // �ڿ��� ����Ǿ��� ���� UI ������Ʈ
        if (previousHP != hp || previousSrc != src || previousElectric != electric)
        {
            UpdateResourceUI();

            // ���� �� ����
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
            //���ּ��� src�Ҹ�
        }
    }

    //�ڿ� �߰�
    public void AddSrc(float amount)
    {
        src += amount;
        Debug.Log($"Src �߰�: {amount}, ���� Src: {src}");
    }

    public void AddElectric(float amount)
    {
        electric += amount;

        Debug.Log($"Electric �߰�: {amount}, ���� Electric: {electric}");
    }

    public void AddHP(float amount)
    {
        hp = Mathf.Min(hp + amount, maximumHP);
        Debug.Log($"HP �߰�: {amount}, ���� HP: {hp}");
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

    public bool ConsumeHP(float amount)
    {
        if (hp > 0)
        {
            hp -= amount;
            Debug.Log($"HP �Ҹ�: {amount}\n���� HP: {hp}");
            if (hp <= 0)
            {
                Debug.Log("�׾���!!");
            }
            return true; // ���������� �Ҹ�
        }
        return false; // ü���� �̹� 0
    }

    // �ڿ� ���� ��ȯ �Լ�
    public float GetSrc() => src;
    public float GetElectric() => electric;
    public float GetHP() => hp;
    public float GetCurAddSrc() => curAddSrc;
    public float GetCurAddHP() => curAddHP;
    public float GetCurAddElectric() => curAddElectric;
    public float GetCurRemoveSrc() => curRemoveSrc;
}
