using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StringEvent : UnityEvent<string>{};

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    [Header("Rooms to visit")]
    [SerializeField] private int maxRoomsToVisit;
    [HideInInspector] public int roomCounter;

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

    [Header("Default Settings")]
    public Boss defaultBoss;
    public Biome defaultBiome;

    // Extra References
    [HideInInspector] public bool isBossDefeated = false;
    private OverlayController fade;

    [HideInInspector] public UnityEvent onEnterRoom;
    [HideInInspector] public UnityEvent onStartCombat;
    [HideInInspector] public UnityEvent onReturnDungeon;
    [HideInInspector] public StringEvent onChangeRoom = new StringEvent();

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            roomCounter = 0;
        }
        else Destroy(this.gameObject);
        
        Random.InitState((int) System.DateTime.Now.Ticks);

        if(party.Count == 0){
            party = new List<Player>();
            AddToListFrom(party, possibleEncounters);
        }
        if(currentBoss == null)
            currentBoss = defaultBoss;
        if(currentBiome == null)
            currentBiome = defaultBiome;
    }

    private void Start() {
        SetupScene(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += SetupScene;

        onReturnDungeon.AddListener(DungeonCleared);
    }

    private void SetupScene(Scene scene, LoadSceneMode mode){
        if(roomCounter == maxRoomsToVisit){
            onChangeRoom?.Invoke("Final");
            return;
        }

        possibleBiomes = new List<Biome>();
        possibleBiomes.AddRange(allBiomes);
        isBossDefeated = false;

        fade = OverlayController.instance;
        fade.enabled = true;

        roomCounter++;

        DungeonManager.instance.SetupDungeon();
        TurnManager.instance.SetupCombat();

        TurnManager.instance.enabled = false;
        DungeonManager.instance.enabled = true;
        Camera.main.transform.position = dungeonCam.localPosition;
        onEnterRoom?.Invoke();
    }

    public void AddToListFrom<T>(List<T> listToAdd, List<T> refList, bool removeItem = true){
        int rand = Random.Range(0, refList.Count);
        listToAdd.Add(refList[rand]);

        if(removeItem){
            refList.RemoveAt(rand);
        }
    }

    public T SelectFrom<T>(List<T> refList, bool removeItem = true){
        int rand = Random.Range(0, refList.Count);
        var value = refList[rand];
        if(removeItem){
            refList.RemoveAt(rand);
        }

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
            if(isBossDefeated){
                TurnManager.instance.gameObject.SetActive(false);
            }
        }
        isCameraInDungeon = !isCameraInDungeon;
    }

    private void DungeonCleared(){
        isBossDefeated = true;
        if(possibleEncounters.Count > 0){
            AddToListFrom(party, possibleEncounters);
        }
    }

    public void NextRoom(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    [Button("Get some info")]
    public void GetStateInfo(){
        Debug.Log("Active player: " + party[0]);
        Debug.Log("Current boss: " + currentBoss);
        Debug.Log("Current biome: " + currentBiome);
    }
}
