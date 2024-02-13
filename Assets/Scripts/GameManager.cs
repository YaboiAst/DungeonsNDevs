using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("Party Manager")]
    [SerializeField] private int maxPartySize;
    [SerializeField] private List<Players> fullParty;
    private List<Players> party;

    [Header("Boss Manager")]
    [SerializeField] GenericEntity boss; 

    [Header("Turn Logic")]
    [SerializeField] private List<GenericEntity> roundOrder;
    private int turn;
    [HideInInspector] public GenericEntity active;

    // EVENTOS ---------------------------------------------
    [HideInInspector] public UnityEvent onRoomEnter;
    [HideInInspector] public UnityEvent onPassTurn;
    [HideInInspector] public UnityEvent onBossKill;

    public GenericEntity GetActiveTurn(){ return active; }

    public GenericEntity GetBoss(){ return boss; }

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
            Destroy(this.gameObject);

        party = new List<Players>();
    }

    private void Start() {
        //AddToParty(fullParty[Random.Range(0, 4)]);

        //Just for tests
        AddToParty(fullParty[0]);
        AddToParty(fullParty[1]);
        AddToParty(fullParty[2]);
        AddToParty(fullParty[3]);
        StartCombat();

        onPassTurn.AddListener(ManageCooldowns);

        turn = -1;
        Next();
    }

    private void StartCombat(){
        roundOrder = new List<GenericEntity>();

        roundOrder.Add(party[0]);
        roundOrder.Add(party[1]);
        roundOrder.Add(party[2]);
        roundOrder.Add(party[3]);
        roundOrder.Add(boss);
    }

    public void Next(){
        turn++;
        if(turn >= roundOrder.Count)
            turn = 0; 

        active = roundOrder[turn];

        onPassTurn?.Invoke();
    }

    private void ManageCooldowns(){
        foreach(Players p in party){
            p.ManageCooldown();
        }
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
