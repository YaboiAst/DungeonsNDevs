using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    DungeonManager dm;
    private Door[] doors;
    private void Awake() {
        doors = GetComponentsInChildren<Door>();
    }

    public void SetupDoors(){
        dm = DungeonManager.instance;

        foreach(Door door in doors){
            door.SetupBiome();
            door.SetupBoss();
        }
    }

    private void OnEnable() {
        foreach(Door door in doors){
            door.enabled = true;
        }
    }

    private void OnDisable() {
        foreach(Door door in doors){
            door.enabled = false;
        }
    }
}
