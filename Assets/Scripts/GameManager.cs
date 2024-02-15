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
    [SerializeField] Bosses boss; 

    [Header("Turn Logic")]
    [SerializeField] private List<GenericEntity> roundOrder;
    private int turn;
    [HideInInspector] public GenericEntity active;

    // EVENTOS ---------------------------------------------
    [HideInInspector] public UnityEvent onRoomEnter;
    [HideInInspector] public UnityEvent onPassTurn;
    [HideInInspector] public UnityEvent onBossKill;

    public Bosses GetBoss(){ return boss; }
    public List<Players> GetParty() { return party; }
    public GenericEntity GetActiveTurn(){ return active; }

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
            Destroy(this.gameObject);

        party = new List<Players>();
        //Just for tests
        AddToParty(fullParty[0]);
        AddToParty(fullParty[1]);
        AddToParty(fullParty[2]);
        AddToParty(fullParty[3]);
    }

    private void Start() {
        //AddToParty(fullParty[Random.Range(0, 4)]);
        StartCombat();
    }

    private void StartCombat(){
        roundOrder = new List<GenericEntity>();

        roundOrder.Add(party[0]);
        roundOrder.Add(party[1]);
        roundOrder.Add(party[2]);
        roundOrder.Add(party[3]);
        roundOrder.Add(boss);

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
