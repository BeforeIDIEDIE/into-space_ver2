using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructSpriteChange : MonoBehaviour
{
     [SerializeField] private Sprite defaultSprite; // �⺻ ��������Ʈ
    [SerializeField] private Sprite closeSprite; // ����� �� ��������Ʈ

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