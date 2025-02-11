using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SirenActive : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private List<Sprite> sprites; 
    [SerializeField] private float spriteChangeInterval = 0.02f; 
    private int currentSpriteIndex = 0;

    private Coroutine spriteCoroutine;

    private void OnEnable()
    {
        if (spriteCoroutine != null)
        {
            StopCoroutine(spriteCoroutine);
        }
        spriteCoroutine = StartCoroutine(AnimateSprite());
    }

    private void OnDisable()
    {
        if (spriteCoroutine != null)
        {
            StopCoroutine(spriteCoroutine);
            spriteCoroutine = null; 
            currentSpriteIndex = 0;
        }
    }

    private IEnumerator AnimateSprite()
    {
        while (true)
        {
            targetImage.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
            yield return new WaitForSeconds(spriteChangeInterval);
        }
    }
}
