using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Camera_zoom : MonoBehaviour
{
    [SerializeField] private Camera targetCamera; 
    [SerializeField] private float targetSize = 10.8f; 
    [SerializeField] private float zoomDuration = 2f;
    [SerializeField] private Vector3 targetPosition = new Vector3(0, -2, -20); 
    [SerializeField] private PixelPerfectCamera pixelPerfectCamera; 

    private void Start()
    {
        StartCoroutine(ZoomAndMoveCamera());
    }

    private IEnumerator ZoomAndMoveCamera()
    {
        pixelPerfectCamera.enabled = false;
        Debug.Log("픽퍼 해제");

        float startSize = targetCamera.orthographicSize;
        Vector3 startPosition = targetCamera.transform.position;
        float elapsedTime = 0f;

        //줌아웃 파트
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;

            targetCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / zoomDuration);
            targetCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / zoomDuration);

            yield return null; 
        }

        targetCamera.orthographicSize = targetSize;
        targetCamera.transform.position = targetPosition;

        Debug.Log("픽퍼 해제 + 목표 이동");
    }

    private void PerformNextTask()
    {
        //다음 작업
    }
}
