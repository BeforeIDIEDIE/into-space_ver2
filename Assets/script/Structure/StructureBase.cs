using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StructureBase : MonoBehaviour
{
    protected bool isNear;
    protected bool isPerformingAction;

    public void playerNeared() => isNear = true;
    public void playerOut() => isNear = false;

    public abstract IEnumerator PerformAction();
}

