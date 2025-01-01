using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float electric = 0f;
    private float curAddElectric = 3f;
    private float maxElectric = 100f;


    private float hp = 100f;
    private float maximunHP = 100f;
    private float curAddHP = 1f;
    

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
    public void AddSrc(int amount)
    {
        src += amount;
        Debug.Log($"Src Added: {amount}, Current Src: {src}");
    }

    public void AddElectric(int amount)
    {
        electric += amount;

        Debug.Log($"Electric Added: {amount}, Current Electric: {electric}");
    }

    public void AddHP(int amount)
    {
        hp = Mathf.Min(hp + amount, maximunHP);
        Debug.Log($"HP Added: {amount}, Current HP: {hp}");
    }

    // 자원 소모
    public bool ConsumeSrc(int amount)
    {
        if (src >= amount)
        {
            src -= amount;
            Debug.Log($"Src Consumed: {amount}\nRemaining Src: {src}");
            return true;
        }
        else
        {
            Debug.Log("No Src!");
            return false;
        }
    }

    public bool ConsumeElectric(int amount)
    {
        if (electric >= amount)
        {
            electric -= amount;
            Debug.Log($"Electric Consumed: {amount}\nRemaining Electric: {electric}");
            return true;//성공적으로 소모
        }
        else
        {
            Debug.Log("No Electric!");
            return false;//소모 실패
        }
    }

    public bool ConsumeHP(int amount)
    {
        if (hp > 0)
        {
            hp -= amount;
            Debug.Log($"HP Consumed: {amount}\nRemaining HP: {hp}");
            if (hp <= 0)
            {
                Debug.Log("dead!");
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
