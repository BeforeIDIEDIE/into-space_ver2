using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularDetector : MonoBehaviour
{
    private StructSpriteChange parentSquare;

    private void Start()
    {
        parentSquare = GetComponentInParent<StructSpriteChange>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentSquare.ChangeToCloseSprite();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentSquare.ChangeToDefaultSprite();
        }
    }
}

