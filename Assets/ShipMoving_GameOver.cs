using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoving_GameOver : MonoBehaviour
{
    [SerializeField]private int idx = 0;
    private Vector3 targetPos;
    private Quaternion targetRotation;
    [SerializeField]private float duration = 5f;
    private Vector3 MovingAction()
    {
        switch (idx)
        {
            case 0:
                {
                    return new Vector3(-15f, -23.5f, 20f);
                }
            case 1:
                {
                    return new Vector3(-20, -17f, 20f);
                }
            case 2:
                {
                    return new Vector3(6.5f, -20.5f, 20f);
                }
            case 3:
                {
                    return new Vector3(1, -14.5f, 20f);
                }
            case 4:
                {
                    return new Vector3(-1.5f, -11.8f, 20f);
                }
            case 5:
                {
                    return new Vector3(7.5f, -20.5f, 20f);
                }
            case 6:
                {
                    return new Vector3(12.44f, -4.5f, 20f);
                }
            default:
                {
                    return transform.position;
                }
        }
    }
    public void startMoving()
    {
        StartCoroutine(PerformAction());
    }

    private IEnumerator PerformAction()
    {
        targetPos = MovingAction() + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        float randomAngle = Random.Range(10f, 30f) * (Random.Range(0, 2) == 0 ? 1 : -1);
        targetRotation = Quaternion.Euler(0f, 0f, randomAngle);
        Vector3 startPos = transform.position;
        Quaternion startRotation = transform.rotation;
        
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);

            yield return null;
        }
        transform.position = targetPos;
        transform.rotation = targetRotation;
    }
}
