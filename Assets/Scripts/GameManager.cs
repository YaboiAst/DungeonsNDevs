using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [SerializeField] private int maxPartySize;
    private List<Players> party;

    private List<Transform> roundOrder;
    private int index;
    private Transform active;
    private Players playersActive;
    // Add Monster Script Ref here

    [HideInInspector] public UnityEvent onRoomEnter;
    [HideInInspector] public UnityEvent onBossKill;

    private void Start() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
            Destroy(this.gameObject);

        //onRoomEnter.AddListener(() => Next());
        onBossKill.AddListener(() => AddToParty(party[0]));
    }

    public void Next(){
        index++;
        if(index >= roundOrder.Count)
            index = 0; 

        active = roundOrder[index];
        playersActive = active.GetComponent<Players>();
        //if(playersActive == null)
    }

    private void AddToParty(Players player){
        roundOrder.Clear();

        if(party.Count < maxPartySize){
            party.Add(player);
        }
    }
}
