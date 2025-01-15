using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStructure : StructureBase
{
    [SerializeField] private speedController controller;

    [SerializeField] private GameObject existingUI; 
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private List<Image> SrcUpgradeSlots;
    [SerializeField] private List<Image> ElectricUpgradeSlots;
    [SerializeField] private List<Image> HealUpgradeSlots;
    [SerializeField] private Sprite upgradedImage;

    [SerializeField] private Button srcUpgradeButton;      
    [SerializeField] private Button electricUpgradeButton; 
    [SerializeField] private Button healUpgradeButton;

    [SerializeField] private TextMeshProUGUI statHP;
    [SerializeField] private TextMeshProUGUI statSrc;
    [SerializeField] private TextMeshProUGUI statElec;

    [SerializeField] private TextMeshProUGUI costHP;
    [SerializeField] private TextMeshProUGUI costSrc;
    [SerializeField] private TextMeshProUGUI costElec;

    private List<float> cost = new List<float>{ 20f, 25f, 30f, 35f, 40f, 45f };
    private List<float> src = new List<float> { 0.5f, 0.5f, 1f, 1f, 2f, 2f };
    private List<float> elec = new List<float> { 0.5f, 0.5f, 1f, 1f, 2f, 2f };
    private List<float> heal = new List<float> { 1f, 1f, 1f, 1f, 1f, 1f };

    private int maxUpgradeIdx = 5;
    private int SrcUpgradeIdx = -1;
    private int ElectricUpgradeIdx = -1;
    private int HealUpgradeIdx = -1;

    private bool isUpgradeUIOn; 

    private void Start()
    {
        existingUI.SetActive(true);
        upgradeUI.SetActive(false);
        isUpgradeUIOn = false;

        DeactivateAllUpgradeSlots(SrcUpgradeSlots);
        DeactivateAllUpgradeSlots(ElectricUpgradeSlots);
        DeactivateAllUpgradeSlots(HealUpgradeSlots);

        SrcUpgradeSlots[SrcUpgradeIdx+1].enabled = true;
        ElectricUpgradeSlots[ElectricUpgradeIdx + 1].enabled = true;
        HealUpgradeSlots[HealUpgradeIdx + 1].enabled = true;

        UpdateStatAndCostText(statSrc, costSrc, GameManager.Instance.GetCurAddSrc(), src, cost, SrcUpgradeIdx);
        UpdateStatAndCostText(statElec, costElec, GameManager.Instance.GetCurAddElectric(), elec, cost, ElectricUpgradeIdx);
        UpdateStatAndCostText(statHP, costHP, GameManager.Instance.GetCurAddHP(), heal, cost, HealUpgradeIdx);
    }

    private void Update()
    {
        //UI 켜고 끔
        if (isNear && Input.GetKeyDown(KeyCode.Space) && !isUpgradeUIOn)
        {
            ActivateUpgradeUI();
        }
        if (isUpgradeUIOn && Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateUpgradeUI();
        }

        if(isUpgradeUIOn)
        {
            srcUpgradeButton.interactable = (SrcUpgradeIdx + 1 < cost.Count) &&
                                         (SrcUpgradeIdx < maxUpgradeIdx) &&
                                         (GameManager.Instance.GetElectric() >= cost[SrcUpgradeIdx + 1]);

            electricUpgradeButton.interactable = (ElectricUpgradeIdx + 1 < cost.Count) &&
                                                 (ElectricUpgradeIdx < maxUpgradeIdx) &&
                                                 (GameManager.Instance.GetElectric() >= cost[ElectricUpgradeIdx + 1]);

            healUpgradeButton.interactable = (HealUpgradeIdx + 1 < cost.Count) &&
                                             (HealUpgradeIdx < maxUpgradeIdx) &&
                                             (GameManager.Instance.GetElectric() >= cost[HealUpgradeIdx + 1]);
        }
    }


    
    
    private void Upgrade(List<Image> upgradeSlots, ref int upgradeIdx, Button upgradeButton)
    {
        upgradeIdx++;
        GameManager.Instance.ConsumeElectric(cost[upgradeIdx]);
        upgradeSlots[upgradeIdx].sprite = upgradedImage;

        if (upgradeSlots == SrcUpgradeSlots)
        {
            GameManager.Instance.AddCurAddSrc(src[upgradeIdx]);
            UpdateStatAndCostText(statSrc, costSrc, GameManager.Instance.GetCurAddSrc(), src, cost, upgradeIdx);
        }
        else if (upgradeSlots == ElectricUpgradeSlots)
        {
            GameManager.Instance.AddCurAddElectric(elec[upgradeIdx]);
            UpdateStatAndCostText(statElec, costElec, GameManager.Instance.GetCurAddElectric(), elec, cost, upgradeIdx);
        }
        else
        {
            GameManager.Instance.AddCurAddHp(heal[upgradeIdx]);
            UpdateStatAndCostText(statHP, costHP, GameManager.Instance.GetCurAddHP(), heal, cost, upgradeIdx);
        }

        if (upgradeIdx < maxUpgradeIdx)
        {
            upgradeSlots[upgradeIdx + 1].enabled = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    public void SrcUpgrade()
    {
        Upgrade(SrcUpgradeSlots, ref SrcUpgradeIdx, srcUpgradeButton);
    }

    public void ElectricUpgrade()
    {
        Upgrade(ElectricUpgradeSlots, ref ElectricUpgradeIdx, electricUpgradeButton);
    }

    public void HealUpgrade()
    {
        Upgrade(HealUpgradeSlots, ref HealUpgradeIdx, healUpgradeButton);
    }

    private void UpdateStatAndCostText(
    TextMeshProUGUI statText,
    TextMeshProUGUI costText,
    float currentValue,
    List<float> incrementList,
    List<float> costList,
    int upgradeIdx)
    {
        if (upgradeIdx + 1 < costList.Count)
        {
            statText.text = $"생산량: {currentValue} → {currentValue + incrementList[upgradeIdx + 1]}";
            costText.text = $"비용: {costList[upgradeIdx + 1]}";
        }
        else
        {
            statText.text = $"생산량: {currentValue}";
            costText.text = "최대 업그레이드!";
        }
    }



    private void ActivateUpgradeUI()
    {
        Time.timeScale = 0f; 
        existingUI.SetActive(false); 
        upgradeUI.SetActive(true);
        isUpgradeUIOn = true;   
    }

    public void DeactivateUpgradeUI()
    {
        Time.timeScale = controller.GetIsTwo() ? 2.0f:1.0f; 
        if (upgradeUI != null)
        {
            upgradeUI.SetActive(false);
        }

        if (existingUI != null)
        {
            existingUI.SetActive(true);
        }
        isUpgradeUIOn = false; 
    }
    public override IEnumerator PerformAction() //안쓴다!- > 상속때문에 어쩔수 없이 작성한겨!
    {
        //기능이 없다!
        yield return new WaitForSeconds(0f);
    }
    public void DeactivateAllUpgradeSlots(List<Image> upgradeSlots)
    {
        foreach (Image slot in upgradeSlots)
        {
            slot.enabled = false;
        }
    }
}