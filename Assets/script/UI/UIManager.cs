using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class UIManager : MonoBehaviour
{
    //업적 기능
    private void Awake()
    {
        savePath = Application.persistentDataPath + "/gameData.json";
        LoadGameData();
    }

    [SerializeField] private GameObject easy;
    [SerializeField] private GameObject medium;
    [SerializeField] private GameObject hard;

    private string savePath;
    private GameData gameData;

    private void LoadGameData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            gameData = new GameData();
            SaveGameData();
        }
    }
    private void SaveGameData()
    {
        if (gameData == null)
        {
            gameData = new GameData();
        }

        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(savePath, json);
    }

    private void UpdateUI()
    {
        easy.SetActive(gameData.isClearedEasy);
        medium.SetActive(gameData.isClearedMedium);
        hard.SetActive(gameData.isClearedHard);
    }
    private void OnEnable()
    {
        UpdateUI();
    }

    [SerializeField]GameObject startUI;
    [SerializeField] private List<GameObject> howTo;
    [SerializeField] GameObject option;
    [SerializeField] private GameObject transformUI;
    [SerializeField] private TypingLikeMan startScene;
    [SerializeField] private SceneTransform choiceScene;
    private int curHowtoIDX;

    private void Start()
    {
        Time.timeScale = 1.0f;
        //스타트 제외 모든 캔버스 안띄움
        OpenStart();
        //startScene.StartScene();
        UpdateUI();
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
        choiceScene.StartUI();
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
