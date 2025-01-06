using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStructure : StructureBase
{
    private void Update()
    {
        if (isNear && Input.GetKey(KeyCode.Space) && !isPerformingAction)
        {
            //업그레이드 UI띄우기
        }
    }
    public override IEnumerator PerformAction() //안쓴다!- > 상속때문에 어쩔수 없이 작성!
    {
        //기능이 없다!
        yield return new WaitForSeconds(0f);
    }
}
