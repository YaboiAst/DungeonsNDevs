using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    [HideInInspector] public static TurnManager instance;
    private GameManager gm;

    [Header("Scene Settings")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] playerPositions;
    [Space(5)]
    //[SerializeField] private GameObject bossPrefab;

    [Header("Party Manager")]
    [SerializeField] private int maxPartySize;
    private List<Players> party;

    [Header("Boss Manager")]
    [SerializeField] Bosses boss; 

    // Turn Logic
    private List<GameObject> roundOrder;
    private int turn;
    [HideInInspector] public GenericEntity active;

    // EVENTOS ---------------------------------------------
    [HideInInspector] public UnityEvent disableCombat;
    [HideInInspector] public UnityEvent onPassTurn;
    [HideInInspector] public UnityEvent onBossKill;
    [HideInInspector] public UnityEvent onPlayerDeath;

    public GenericEntity GetActiveTurn(){ return active; }
    public List<Players> GetParty() { return party; }
    public Bosses GetBoss(){ return boss; }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else return;
    }

    public void SetupCombat(){
        if(gm == null){
            gm = GameManager.instance;
        }

        party = new List<Players>();
        for(int i = 0; i < gm.party.Count; i++){
            GameObject go = Instantiate(playerPrefab, playerPositions[i].position, playerPositions[i].rotation, this.transform);
            Players player = go.GetComponent<Players>();
            player.SetPlayerInfo(gm.party[i]);

            party.Add(player);
        }

        boss.SetBossInfo(gm.currentBoss);

        List<GameObject> auxList = new List<GameObject>();
        foreach(Players player in party){
            auxList.Add(player.gameObject);
        }
        auxList.Add(boss.gameObject);

        roundOrder = new List<GameObject>();
        int size = auxList.Count;
        for(int i = 0; i < size; i++){
            gm.AddToListFrom(roundOrder, auxList);
        }

        turn = -1;
        Next();
    }

    public void Next(){
        turn++;
        if(turn >= roundOrder.Count)
            turn = 0; 

        active = roundOrder[turn].GetComponent<GenericEntity>();

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

    private void OnDisable() {
        disableCombat?.Invoke();
    }

    [ContextMenu("NextTurn")] public void DoNext(){
        Next();
    }
}
