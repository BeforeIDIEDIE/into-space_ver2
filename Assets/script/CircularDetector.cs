using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularDetector : MonoBehaviour
{
    private StructSpriteChange parentSquare;
    private StructureBase playerDoing;

    private void Start()
    {
        parentSquare = GetComponentInParent<StructSpriteChange>();
        playerDoing = GetComponentInParent<StructureBase>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentSquare.ChangeToCloseSprite();
            playerDoing.playerNeared();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentSquare.ChangeToDefaultSprite();
            playerDoing.playerOut();
        }
    }
}

