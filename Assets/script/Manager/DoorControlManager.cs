using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControlManager : MonoBehaviour
{
    [SerializeField] private List<DoorProcess> doorList;

    private DoorProcess curCantUsedDoor;
    private void Start()
    {
        InitAllDoor();
        RandomDoorClosed();
    }

    private void InitAllDoor()
    {
        foreach (DoorProcess door in doorList)
        {
            door.initDoorStatus();
        }
    }


    public void DayOffFunction()
    {
        RandomDoorClosed();
    }

    private void RandomDoorClosed()
    {
        if(curCantUsedDoor != null)
        {
            curCantUsedDoor.OffDoorCantOperate();
        }
        DoorProcess randDoor;
        do
        {
            randDoor = doorList[Random.Range(0, doorList.Count)];
        }
        while (randDoor.GetIsOperating());
        randDoor.OnDoorCantOperate();
        curCantUsedDoor = randDoor;
    }

}
