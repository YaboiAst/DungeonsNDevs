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

    [SerializeField] private List<Transform> roundOrder;
    private int turn;
    [HideInInspector] public Transform active;
    private Players playersActive;
    // Add Monster Script Ref here

    [HideInInspector] public UnityEvent onRoomEnter;
    [HideInInspector] public UnityEvent onPassTurn;
    [HideInInspector] public UnityEvent onBossKill;

    public Transform GetActiveTurn(){
        return active;
    }

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
            Destroy(this.gameObject);
    }

    private void Start() {
        //onRoomEnter.AddListener(() => Next());
        onBossKill.AddListener(() => AddToParty(party[0]));

        turn = -1;
        Next();
    }

    public void Next(){
        turn++;
        if(turn >= roundOrder.Count)
            turn = 0; 

        active = roundOrder[turn];
        playersActive = active.GetComponent<Players>();
        //if(playersActive == null)

        onPassTurn?.Invoke();
    }

    private void AddToParty(Players player){
        roundOrder.Clear();

        if(party.Count < maxPartySize){
            party.Add(player);
        }
    }

    [ContextMenu("NextTurn")]
    public void DoNext(){
        Next();
    }
}
