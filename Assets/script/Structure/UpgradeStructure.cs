using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStructure : StructureBase
{

    [SerializeField] private GameObject existingUI; 
    [SerializeField] private GameObject upgradeUI; 

    private bool isUpgradeUIOn; 

    private void Start()
    {
        isUpgradeUIOn = false;
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
}
