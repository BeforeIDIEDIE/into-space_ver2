using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularDetector : MonoBehaviour
{
    private StructSpriteChange parentSquare;
    private player_action playerDoing;

    private void Start()
    {
        parentSquare = GetComponentInParent<StructSpriteChange>();
        playerDoing = GetComponentInParent<player_action>();
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

