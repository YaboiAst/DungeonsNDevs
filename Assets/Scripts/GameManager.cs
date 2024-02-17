using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("Biome")]
    public List<Biome> allBiomes;
    [HideInInspector] public List<Biome> possibleBiomes;
    [HideInInspector] public Biome currentBiome;

    [Header("Party")]
    public List<Player> possibleEncounters;
    [HideInInspector] public List<Player> party;
    
    [Header("Boss")]
    [HideInInspector] public Boss currentBoss;

    [Space(10)]
    
    [BoxGroup("Vision Management")]
    [SerializeField] private Transform dungeonCam, combatCam;
    private bool isCameraInDungeon = true;

    [HideInInspector] public UnityEvent onEnterRoom;
    [HideInInspector] public UnityEvent onStartCombat;
    [HideInInspector] public UnityEvent onSelectDoor;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
            Destroy(this.gameObject);

        UnityEngine.Random.InitState((int) long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
        possibleBiomes = allBiomes;

        if(party.Count == 0){
            party = new List<Player>();
            AddToListFrom(party, possibleEncounters);
        }

        if(currentBoss == null){
            currentBoss = defaultBoss;
        }

        if(currentBiome == null){
            currentBiome = defaultBiome;
        }
    }

    public Boss defaultBoss;
    public Biome defaultBiome;
    private void Start() {
        DungeonManager.instance.SetupDungeon();

        TurnManager.instance.SetupCombat();

        TurnManager.instance.enabled = false;
        DungeonManager.instance.enabled = true;
        Camera.main.transform.position = dungeonCam.localPosition;
    }

    public void AddToListFrom<T>(List<T> listToAdd, List<T> refList){
        if(refList == null){
            Debug.Log("Lista inválida");
            return;
        }
        if(refList.Count == 0){
            Debug.Log("Lista vazia");
        }

        int rand = UnityEngine.Random.Range(0, refList.Count);

        listToAdd.Add(refList[rand]);
        refList.RemoveAt(rand);
    }

    public T SelectFrom<T>(List<T> refList){
        int rand = UnityEngine.Random.Range(0, refList.Count);
        var value = refList[rand];
        refList.RemoveAt(rand);

        return value;
    }

    public void SwitchVisions(){
        
        if(isCameraInDungeon){
            TurnManager.instance.enabled = true;
            DungeonManager.instance.enabled = false;
            Camera.main.transform.position = combatCam.localPosition;
        }
        else{
            TurnManager.instance.enabled = false;
            DungeonManager.instance.enabled = true;
            Camera.main.transform.position = dungeonCam.localPosition;
        }
        isCameraInDungeon = !isCameraInDungeon;
    }

    [Button]
    public void GoToCombat(){
        onStartCombat?.Invoke();
    }
}
