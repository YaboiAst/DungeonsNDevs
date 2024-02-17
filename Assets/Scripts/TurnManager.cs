using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    [HideInInspector] public static TurnManager instance;
    private GameManager gm;

    [Header("Party Manager")]
    [SerializeField] private int maxPartySize;
    [SerializeField] private List<Players> fullParty;
    private List<Players> party;

    [Header("Boss Manager")]
    [SerializeField] Bosses boss; 

    // Turn Logic
    private List<GenericEntity> roundOrder;
    private int turn;
    [HideInInspector] public GenericEntity active;

    // EVENTOS ---------------------------------------------
    [HideInInspector] public UnityEvent onRoomEnter;
    [HideInInspector] public UnityEvent onPassTurn;
    [HideInInspector] public UnityEvent onBossKill;

    public GenericEntity GetActiveTurn(){ return active; }
    public List<Players> GetParty() { return gm.GetParty(); }
    public Bosses GetBoss(){ return gm.GetBoss(); }

    private void Awake() {
        //Just for tests
        party = new List<Players>();
        AddToParty(fullParty[0]);
        AddToParty(fullParty[1]);
        AddToParty(fullParty[2]);
        AddToParty(fullParty[3]);
    }

    private void Start() { 
        gm = GameManager.instance;
        UnityEngine.Random.InitState((int) long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
        StartCombat(); 
    }

    private void StartCombat(){
        List<GenericEntity> auxList = new List<GenericEntity>();
        auxList.AddRange(party);
        auxList.Add(boss);

        roundOrder = new List<GenericEntity>();
        for(int i = 0; i < maxPartySize + 1; i++){
            int rand = UnityEngine.Random.Range(0, auxList.Count);
            roundOrder.Add(auxList[rand]);
            auxList.RemoveAt(rand);
        }

        turn = -1;
        Next();
    }

    public void Next(){
        turn++;
        if(turn >= roundOrder.Count)
            turn = 0; 

        active = roundOrder[turn];

        if(active.isStuned){
            active.isStuned = false;
            Next();
        }

        if(active.isPlayer()){
            active.GetComponent<Players>().ManageCooldown();
        }
        else{
            active.GetComponent<Bosses>().Invoke("TakeAction", 2f);
        }

        onPassTurn?.Invoke();
    }

    private void AddToParty(Players player){
        //roundOrder.Clear();

        if(party.Count < maxPartySize){
            party.Add(player);
        }
    }

    [ContextMenu("NextTurn")] public void DoNext(){
        Next();
    }
}
