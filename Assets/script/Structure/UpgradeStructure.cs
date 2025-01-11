using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStructure : StructureBase
{

    [SerializeField] private GameObject existingUI; 
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private List<Image> SrcUpgradeSlots;
    [SerializeField] private List<Image> ElectricUpgradeSlots;
    [SerializeField] private List<Image> HealUpgradeSlots;
    [SerializeField] private Sprite upgradedImage;

    [SerializeField] private Button srcUpgradeButton;      
    [SerializeField] private Button electricUpgradeButton; 
    [SerializeField] private Button healUpgradeButton;

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
    }

    private void Update()
    {
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
            if (SrcUpgradeIdx >= maxUpgradeIdx)
            {
                srcUpgradeButton.interactable = false;
            }
            if (ElectricUpgradeIdx >= maxUpgradeIdx)
            {
                electricUpgradeButton.interactable = false;
            }
            if (HealUpgradeIdx >= maxUpgradeIdx)
            {
                healUpgradeButton.interactable = false;
            }
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

    private void Upgrade(List<Image> upgradeSlots, ref int upgradeIdx, Button upgradeButton)
    {
        upgradeIdx++;
        upgradeSlots[upgradeIdx].sprite = upgradedImage;

        if (upgradeIdx < maxUpgradeIdx)
        {
            upgradeSlots[upgradeIdx + 1].enabled = true;
        }
        else
        {
            upgradeButton.interactable = false;
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
        Time.timeScale = 1f; 
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

//public void SrcUpgrade()
//{

//    SrcUpgradeIdx++;
//    SrcUpgradeSlots[SrcUpgradeIdx].sprite = upgradedImage;
//    if (SrcUpgradeIdx < 5)
//    {
//        SrcUpgradeSlots[SrcUpgradeIdx + 1].enabled = true;
//    }
//}
//public void ElectricUpgrade()
//{

//    ElectricUpgradeIdx++;
//    ElectricUpgradeSlots[ElectricUpgradeIdx].sprite = upgradedImage;
//    if(ElectricUpgradeIdx < 5)
//    {
//        ElectricUpgradeSlots[ElectricUpgradeIdx + 1].enabled = true;
//    }
//}
//public void HealUpgrade()
//{

//    HealUpgradeIdx++;
//    HealUpgradeSlots[HealUpgradeIdx].sprite = upgradedImage;
//    if (HealUpgradeIdx < 5)
//    {
//        HealUpgradeSlots[HealUpgradeIdx + 1].enabled = true;
//    }
//}