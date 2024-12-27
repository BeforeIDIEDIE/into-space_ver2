using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructSpriteChange : MonoBehaviour
{
     [SerializeField] private Sprite defaultSprite; // 기본 스프라이트
    [SerializeField] private Sprite closeSprite; // 가까울 때 스프라이트

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
    }

    public void ChangeToCloseSprite()
    {
        spriteRenderer.sprite = closeSprite;
    }

    public void ChangeToDefaultSprite()
    {
        spriteRenderer.sprite = defaultSprite;
    }
}