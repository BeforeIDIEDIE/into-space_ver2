using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrackManager : MonoBehaviour
{
    [SerializeField] private GameObject crackPrefab;
    [SerializeField] private TextMeshProUGUI crackTimer;
    [SerializeField] private GameObject siren;
    [SerializeField] private GameObject blackBoard;

    [SerializeField] private AudioSource crackSound;
    [SerializeField] private AudioSource fixSound;
    private List<Vector2> crackPoint = new List<Vector2>
    {
        new Vector2(-6, -4), new Vector2(-6, -1), new Vector2(7, -1), new Vector2(7, -4),
        new Vector2(4, -7), new Vector2(4, 1.5f), new Vector2(-4.5f, 1.5f), new Vector2(1, -8),
        new Vector2(-1.5f, -8), new Vector2(-1.5f, 3), new Vector2(1, 3), new Vector2(-4.5f, -7)
    };

    private GameObject currentCrack = null;
    private Coroutine crackCoroutine;

    public void StartCrackCycle()
    {
        if (crackCoroutine != null)
        {
            StopCoroutine(crackCoroutine);
        }
        crackCoroutine = StartCoroutine(CrackSpawnRoutine());
    }

    private IEnumerator CrackSpawnRoutine()
    {
        float dayDuration = GameManager.Instance.GetDayDuration();

        float waitTime1 = Random.Range(dayDuration / 4, dayDuration / 2);
        yield return new WaitForSeconds(waitTime1);
        CreateRandomCrack();

        float remainingTime = dayDuration - waitTime1;
        if (remainingTime > dayDuration / 4)
        {
            float waitTime2 = Random.Range(dayDuration / 4, remainingTime / 2);
            yield return new WaitForSeconds(waitTime2);
            CreateRandomCrack();
        }
    }

    public void CreateRandomCrack()
    {
        if (currentCrack != null)//혹시나 호오오오옥시나 하는마음에 작성함
        {
            return;
        }
        crackSound.Play();
        Vector2 randomPosition = crackPoint[Random.Range(0, crackPoint.Count)];
        currentCrack = Instantiate(crackPrefab, randomPosition, Quaternion.identity);
        StartCoroutine(CrackCountdown());
    }

    private IEnumerator CrackCountdown()
    {
        float timer = 25f;
        crackTimer.gameObject.SetActive(true);
        blackBoard.gameObject.SetActive(true);
        siren.gameObject.SetActive(true);

        while (timer > 0)
        {
            if (GameManager.Instance.IsGameOver() || GameManager.Instance.IsGameWin())
            {
                crackTimer.gameObject.SetActive(false);
                yield break;
            }

            if (currentCrack == null)
            {
                crackTimer.gameObject.SetActive(false);
                blackBoard.gameObject.SetActive(false);
                siren.gameObject.SetActive(false);
                fixSound.Play();
                yield break;
            }

            crackTimer.text = $"{timer:F1}SEC";
            timer -= Time.deltaTime;
            yield return null;
        }

        if (currentCrack != null && !GameManager.Instance.IsGameOver() && !GameManager.Instance.IsGameWin())
        {
            GameManager.Instance.TriggerGameOver();
            GameManager.Instance.TypingDyingMessage("사유 : 크랙");
            GameManager.Instance.GameOver2Start();
        }
    }
}
