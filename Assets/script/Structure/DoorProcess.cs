using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProcess : MonoBehaviour
{
    [SerializeField] private List<GameObject> doorParts;
    [SerializeField] private Sprite openSprite; 
    [SerializeField] private Sprite closedSprite; 
    private int curActivedDoorPartsIDX = 37;
    private Coroutine doorCoroutine;
    [SerializeField]private bool doorCantOperate = false;
    [SerializeField] private bool isOperating = false;

    public void initDoorStatus()
    {
        OffDoorCantOperate();
    }
    public bool GetIsOperating() => isOperating;

    public void OnDoorCantOperate()
    {
        doorCantOperate = true;
        foreach (GameObject doorPart in doorParts)
        {
            SetSprite(doorPart, closedSprite);
        }
    }

    public void OffDoorCantOperate()
    {
        doorCantOperate = false;
        foreach (GameObject doorPart in doorParts)
        {
            SetSprite(doorPart, openSprite);
            doorPart.SetActive(true);
        }
    }

    private void SetSprite(GameObject doorPart, Sprite sprite)
    {
        SpriteRenderer renderer = doorPart.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(doorCantOperate)
        {
            return;
        }
        doorCoroutine = StartCoroutine(AdjustDoorIndex(1));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (doorCantOperate)
        {
            return;
        }

        if (doorCoroutine != null)
        { 
            StopCoroutine(doorCoroutine);
        }

        doorCoroutine = StartCoroutine(AdjustDoorIndex(37));
    }

    private IEnumerator AdjustDoorIndex(int targetIndex)
    {
        isOperating = true;
        float doorSpeed = 0.02f;
        Debug.Log("문작동");
        while (curActivedDoorPartsIDX != targetIndex)
        {
            if (curActivedDoorPartsIDX > targetIndex)
            {
                curActivedDoorPartsIDX--;
                doorParts[curActivedDoorPartsIDX].SetActive(false);
            }
            else if (curActivedDoorPartsIDX < targetIndex)
            {
                doorParts[curActivedDoorPartsIDX].SetActive(true);
                curActivedDoorPartsIDX++;
            }

            float elapsedTime = 0f;
            while (elapsedTime < doorSpeed)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        isOperating = false;
        Debug.Log("문작동 끝");
    }
}
