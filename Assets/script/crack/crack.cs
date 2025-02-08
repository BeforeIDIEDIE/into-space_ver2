using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crack : StructureBase
{
    [SerializeField] private SpriteRenderer targetRenderer;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private float spriteChangeInterval = 0.001f;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private AudioSource fixCrack;
    private void Start()
    {
        targetRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(AnimateSprite());
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver() || GameManager.Instance.IsGameWin())
        {
            return;
        }
        if (isNear && Input.GetKey(KeyCode.Space))
        {
            fixCrack.Play();
            Destroy(gameObject);
        }
    }
    public override IEnumerator PerformAction()
    {
        yield return null;
    }


    public IEnumerator AnimateSprite()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            if (targetRenderer != null)
            {
                targetRenderer.sprite = sprites[i];
            }
            yield return new WaitForSeconds(spriteChangeInterval);
        }
        targetRenderer.sprite = defaultSprite;
    }
}
