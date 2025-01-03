using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private float smoothSpeed; 
    [SerializeField] private float delayTime; 

    private Vector3 targetPosition; 
    private Queue<Vector3> positionHistory;

    private void Start()
    {
        positionHistory = new Queue<Vector3>();
        transform.position = new Vector3(-6, 3, 5);
    }

    private void LateUpdate()
    {
        Vector3 position = new Vector3(player.position.x, player.position.y, -20);
        positionHistory.Enqueue(position);

        if (positionHistory.Count > Mathf.RoundToInt(delayTime / Time.deltaTime))
        {
            targetPosition = positionHistory.Dequeue();
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
