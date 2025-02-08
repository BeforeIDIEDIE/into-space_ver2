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

    private void Start()
    {
        StartCoroutine(AnimateSprite());
    }
    public IEnumerator AnimateSprite()
    {
        while (true)
        {
            targetImage.sprite = sprites[currentSpriteIndex];
            currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count; 
            yield return new WaitForSeconds(spriteChangeInterval);
        }
    }
}
