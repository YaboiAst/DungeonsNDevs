using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private GameManager gm;
    
    private Biome doorBiome;
    [SerializeField] private int nEasy, nMedium, nHard;

    private void Start() {
        gm = GameManager.instance;
        UnityEngine.Random.InitState((int) long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
    }

    public Biome SelectBiome(){
        List<Biome> possibleBiomes = gm.possibleBiomes;
        possibleBiomes.Remove(gm.GetCurrentBiome());

        int rand = UnityEngine.Random.Range(0, possibleBiomes.Count);
        return possibleBiomes[rand];
    }

    public void SetBiome(){
        doorBiome = SelectBiome();
        GetComponent<SpriteRenderer>().sprite = doorBiome.biomeDoor;
    }

    public Boss SelectBoss(){
        SetBiome();

        Boss selectedBoss;
        List<Boss> possibleBosses = new List<Boss>();
        int i, randIdx;

        for(i = 0; i < nEasy; i++){
            randIdx = UnityEngine.Random.Range(0, doorBiome.easyBosses.Count);
            possibleBosses.Add(doorBiome.easyBosses[randIdx]);
            doorBiome.easyBosses.RemoveAt(randIdx);
        }

        for(i = 0; i < nMedium; i++){
            randIdx = UnityEngine.Random.Range(0, doorBiome.mediumBosses.Count);
            possibleBosses.Add(doorBiome.mediumBosses[randIdx]);
            doorBiome.mediumBosses.RemoveAt(randIdx);
        }

        for(i = 0; i < nHard; i++){
            randIdx = UnityEngine.Random.Range(0, doorBiome.hardBosses.Count);
            possibleBosses.Add(doorBiome.hardBosses[randIdx]);
            doorBiome.hardBosses.RemoveAt(randIdx);
        }

        randIdx = UnityEngine.Random.Range(0, possibleBosses.Count);
        selectedBoss = possibleBosses[randIdx];
        return selectedBoss;
    }

    public void SetBoss(){
        gm.currentBoss = SelectBoss(); 
    }
}
