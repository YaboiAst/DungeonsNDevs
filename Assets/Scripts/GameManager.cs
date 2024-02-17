using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    [Header("Biome")]
    public List<Biome> possibleBiomes;
    [HideInInspector] public Biome currentBiome;

    [Header("Party")]
    [SerializeField] private List<Player> possibleEncounters;
    private List<Players> party;
    
    [Header("Boss")]
    [SerializeField] private Bosses boss;
    [HideInInspector] public Boss currentBoss;

    [Space(10)]
    
    [SerializeField] private GameObject _dungeon, _combat;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else 
            Destroy(this.gameObject);
    }

    private void Start() {
        _dungeon.SetActive(true);
        _combat.SetActive(false);
    }

    public List<Players> GetParty() { return party; }
    public Bosses GetBoss(){ return boss; }
    public Biome GetCurrentBiome() { return currentBiome; }

    private void SwitchVisions(){
        bool mode = _dungeon.activeSelf;

        _dungeon.SetActive(!mode);
        _combat.SetActive(mode);
    }

    [Button]
    public void GoToCombat(){
        SwitchVisions();
    }
}
