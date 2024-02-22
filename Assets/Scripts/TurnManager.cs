using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    [HideInInspector] public static TurnManager instance;

    private GameManager gm;

    [Header("Scene Settings")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] playerPositions;

    [Header("Party Manager")]
    private List<Players> party;

    [Header("Boss Manager")]
    [SerializeField] Monsters boss; 

    // TURN LOGIC --------------------
    private List<GameObject> roundOrder;
    private int turn;
    [HideInInspector] public GenericEntity active;

    // Action HUD Management
    private ActionsManager actionsManager;
    public ActionsManager GetActionsManager(){ return actionsManager; }

    // EVENTOS --------------------
    [HideInInspector] public UnityEvent onPassTurn;
    [HideInInspector] public UnityEvent onBossKill;

    public GenericEntity GetActiveTurn(){ return active; }
    public List<Players> GetParty() { return party; }
    public Monsters GetBoss(){ return boss; }

    public List<GameObject> GetTurnOrder() { return roundOrder; }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else return;

        actionsManager = GetComponentInChildren<ActionsManager>();
    }

    public void SetupCombat(){
        gm = GameManager.instance;

        // DEFINE CHARACTERS INFO --------------------
        party = new List<Players>();
        for(int i = 0; i < gm.party.Count; i++){
            GameObject go = Instantiate(playerPrefab, playerPositions[i].position, playerPositions[i].rotation, this.transform);
            go.transform.SetAsFirstSibling();
            go.GetComponent<SpriteRenderer>().sortingOrder = 6 + i;

            Players player = go.GetComponent<Players>();
            player.SetPlayerInfo(gm.party[i]);
            party.Add(player);
        }

        boss.SetMonsterInfo(gm.currentBoss);


        // DEFINE TURN ORDER --------------------
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
    }

    private void OnDisable() {
        actionsManager.enabled = false;
    }

    private void OnEnable() {
        // Quando a cena carrega...
        if(gm == null)
            return;

        actionsManager.enabled = true;

        turn = -1;
        Next();
    }

    public void Next(){
        // Empty
        if(roundOrder.Count == 0)
            return;

        // Updates order index
        turn++;
        if(turn >= roundOrder.Count)
            turn = 0; 

        active = roundOrder[turn].GetComponent<GenericEntity>();

        if(active.isStuned){
            // TODO onEndStun Event here
            active.isStuned = false;
            Next();
        }

        if(active.isPlayer()){
            Players activePlayer = active.GetComponent<Players>();
            actionsManager.SetActivePlayer(activePlayer);
            actionsManager.UpdatePlayerActionHUD();

            activePlayer.ManageCooldown();
        }
        else{
            Monsters activeMonster = active.GetComponent<Monsters>();
            actionsManager.SetActivePlayer(null);
            actionsManager.UpdateMonsterActionHUD();

            activeMonster.Invoke("TakeAction", 2f);
        }

        onPassTurn?.Invoke();
    }

    public void ClearTurnOrder(){
        roundOrder.RemoveRange(0, roundOrder.Count);
    }

    [ContextMenu("NextTurn")] public void DoNext(){
        Next();
    }
}
