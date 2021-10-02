using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] Door entryDoor;
    [SerializeField] Door exitDoor;
    [SerializeField] BattleSystem battleSystem;

    private void Start() {
        battleSystem.OnBattleStarted += BattleSystem_OnBattleStarted;
        battleSystem.OnBattleOver += BattleSystem_OnBattleOver;
    }
     void BattleSystem_OnBattleStarted(object sender, System.EventArgs e)
    {
        entryDoor.CloseDoor();
    }
    void BattleSystem_OnBattleOver(object sender, System.EventArgs e)
    {
        exitDoor.OpenDoor();
        entryDoor.OpenDoor();

    }


   
}
