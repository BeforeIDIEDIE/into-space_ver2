using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProcess : MonoBehaviour
{

    [SerializeField] private List<GameObject> doorParts;
    [SerializeField] private Sprite openSprite; 
    [SerializeField] private Sprite closedSprite; 
    private int curActivedDoorPartsIDX = 36;
    private Coroutine doorCoroutine;
    [SerializeField]private bool doorCantOperate = false;

    private void Start()
    {
        // 모든 문 조각 활성화
        foreach (GameObject doorPart in doorParts)
        {
            doorPart.SetActive(true);
            SetSprite(doorPart, openSprite);
        }
        curActivedDoorPartsIDX = 36;
    }
    private void SetSprite(GameObject doorPart, Sprite sprite)
    {
        SpriteRenderer renderer = doorPart.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = sprite;
        }
    }

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

        doorCoroutine = StartCoroutine(AdjustDoorIndex(36));
    }

    private IEnumerator AdjustDoorIndex(int targetIndex)
    {
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

            yield return new WaitForSeconds(0.02f);
        }
    }

}
