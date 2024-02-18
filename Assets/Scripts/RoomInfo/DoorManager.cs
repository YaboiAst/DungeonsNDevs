using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Door[] doors;
    void Start(){
        doors = GetComponentsInChildren<Door>();
    }

    public void SetupDoors(){
        foreach(Door door in doors){
            door.SetupBiome();
            door.SetupBoss();
        }
    }
}
