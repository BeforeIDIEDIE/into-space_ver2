using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject SRCUI;
    [SerializeField] GameObject EventUI;
    [SerializeField] GameObject UPGRADEUI;
    [SerializeField] GameObject PauseUI;
    [SerializeField] speedController speedController;
    [SerializeField] private Button resumeBTN;
    [SerializeField] private Button retryBTN;
    [SerializeField] private Button endBTN;

    [SerializeField] private AudioSource BTNSound;

    void Update()
    {
        if (UPGRADEUI.activeSelf)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&!GameManager.Instance.IsGameOver())
        {   
            if(SRCUI.activeSelf)
            {
                TogglePause();
            }
            else if(PauseUI.activeSelf)
            {
                TogglePause();
                BTNSound.Play();
            }
        }
    }

    public void TogglePause()
    {
        if (PauseUI.activeSelf)
        {
            PauseUI.SetActive(false);
            SRCUI.SetActive(true);
            Time.timeScale = speedController.GetIsTwo() ? 2.0f : 1.0f;
        }
        else
        {
            SRCUI.SetActive(false);
            PauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
