using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]GameObject startUI;
    [SerializeField] private List<GameObject> howTo;
    [SerializeField] GameObject option;
    [SerializeField] private GameObject transformUI;
    private int curHowtoIDX;

    private void Start()
    {
        //스타트 제외 모든 캔버스 안띄움
        OpenStart();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleWithStart();
        }
    }
    private void OpenStart()
    {
        startUI.SetActive(true);
        foreach (GameObject go in howTo)
        {
            go.SetActive(false);
        }
        option.SetActive(false);
        transformUI.SetActive(false);
    }

    public void OpenOptionCanvas()
    {
        startUI.SetActive(false);
        option.SetActive(true);
    }
    public void OpenTransformCanvas()
    {
        startUI.SetActive(false);
        transformUI.SetActive(true);
    }
    public void OpenHowToCanvas()
    {
        startUI.SetActive(false);
        howTo[0].SetActive(true);
        curHowtoIDX = 0;
    }
    public void nextHowtoCanvas()
    {
        howTo[curHowtoIDX].SetActive(false);
        howTo[++curHowtoIDX].SetActive(true);
    }
    public void prevHowtoCanvas()
    {
        howTo[curHowtoIDX].SetActive(false);
        howTo[--curHowtoIDX].SetActive(true);
    }
    public void ToggleWithStart()
    {
        OpenStart();
    }
    public void ExitGame()
    {
        Debug.Log("게임 종료"); 
        Application.Quit();
    }
}
