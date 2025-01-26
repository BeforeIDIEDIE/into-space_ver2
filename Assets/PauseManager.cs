using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject SRCUI;
    [SerializeField] GameObject EventUI;
    [SerializeField] GameObject UPGRADEUI;
    [SerializeField] GameObject PauseUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SRCUI.activeSelf)
            {

            }
        }
    }
}
