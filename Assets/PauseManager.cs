using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject SRCUI;
    [SerializeField] GameObject EventUI;
    [SerializeField] GameObject UPGRADEUI;
    [SerializeField] GameObject PauseUI;
    [SerializeField] speedController speedController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SRCUI.activeSelf)
            {
                TogglePause();
            }
            else if(PauseUI.activeSelf)
            {
                TogglePause();
            }
        }
    }

    private void TogglePause()
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
}
