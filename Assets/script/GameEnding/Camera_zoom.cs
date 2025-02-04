using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class Camera_zoom : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float targetSize = 10.8f;
    [SerializeField] private float zoomDuration = 3f;
    [SerializeField] private Vector3 targetPosition = new Vector3(0, -2, -20);
    [SerializeField] private PixelPerfectCamera pixelPerfectCamera;
    [SerializeField] private List<GameObject> spritesToReveal; // 드러낼 스프라이트 오브젝트 리스트
    [SerializeField] private GameObject ship;
    [SerializeField] private GameObject exceptShip;
    [SerializeField] private GameObject planet;
    [SerializeField] private CanvasGroup explosionUI_CG;
    [SerializeField] private GameObject explosionUI_Object;
    [SerializeField] private List<ShipMoving_GameOver> shipParts;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject WinUI;
    [SerializeField] private AudioSource ZoomoutSound;
    [SerializeField] private AudioSource GameOverSound;
    [SerializeField] private AudioSource GameWinSound;

    private float uiFadeDuration = 3f;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    private void Start()
    {
        
        // 스프라이트 오브젝트의 SpriteRenderer 컴포넌트를 가져오기
        foreach (GameObject spriteObject in spritesToReveal)
        {
            SpriteRenderer renderer = spriteObject.GetComponent<SpriteRenderer>();
            spriteRenderers.Add(renderer);
            Color initialColor = renderer.color;
            initialColor.a = 0f;
            renderer.color = initialColor;
        }
    }

    public void GameOver2Start()
    {
        StartCoroutine(ZoomAndRevealObjects(FadeUIEffect()));
    }
    public void WinStart()
    {
        StartCoroutine(ZoomAndRevealObjects(TargetClose()));
    }

    private IEnumerator ZoomAndRevealObjects(IEnumerator Coroutine)
    {
        ZoomoutSound.Play();
        ship.SetActive(true);
        pixelPerfectCamera.enabled = false;
        Debug.Log("픽퍼 해제");

        float startSize = targetCamera.orthographicSize;
        Vector3 startPosition = targetCamera.transform.position;
        float elapsedTime = 0f;

        //줌아웃 + 스프라이트이동
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;

            //줌아웃 + obj이동
            targetCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / zoomDuration);
            targetCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / zoomDuration);

            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                Color color = renderer.color;
                color.a = Mathf.Lerp(0f, 1f, elapsedTime / zoomDuration);
                renderer.color = color;
            }
            yield return null; 
        }

        targetCamera.orthographicSize = targetSize;
        targetCamera.transform.position = targetPosition;

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            Color finalColor = renderer.color;
            finalColor.a = 1f; // Alpha 값 최종 설정
            renderer.color = finalColor;
        }
        exceptShip.SetActive(false);
        StartCoroutine(Coroutine);

    }

    private IEnumerator TargetClose()
    {
        planet.SetActive(true);
        Vector3 startPos = planet.transform.position;
        Vector3 endPos = new Vector3(69, 0, 0);
        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            planet.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / zoomDuration);
            yield return null;
        }
        planet.transform.position = endPos;
        Time.timeScale = 0f;
        GameWinSound.Play();
        WinUI.SetActive(true);
    }

    private IEnumerator FadeUIEffect()
    {
        bool hasMovedShips = false;
        explosionUI_Object.gameObject.SetActive(true);
        //UI 페이드인
        float elapsedTime = 0f;
        while (elapsedTime < uiFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            explosionUI_CG.alpha = Mathf.Lerp(0f, 1f, elapsedTime / uiFadeDuration);
            yield return null;
        }
        explosionUI_CG.alpha = 1f;

        yield return new WaitForSeconds(0.5f);

        uiFadeDuration += 0.5f;
        //UI 페이드아웃
        elapsedTime = 0f;
        while (elapsedTime < uiFadeDuration)
        {
            if(elapsedTime>0.2f && !hasMovedShips)
            {
                hasMovedShips=true;
                foreach (ShipMoving_GameOver shipPart in shipParts)
                {
                    shipPart.startMoving();
                }
            }
            elapsedTime += Time.deltaTime;
            explosionUI_CG.alpha = Mathf.Lerp(1f, 0f, elapsedTime / uiFadeDuration);
            yield return null;
        }

        explosionUI_CG.alpha = 0f;
        explosionUI_Object.SetActive(false);
        GameOverSound.Play();
        GameManager.Instance.TriggerGameOverUI();
    }
}
