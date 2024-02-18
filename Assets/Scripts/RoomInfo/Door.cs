using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameManager gm;
    
    private Biome doorBiome;
    private Boss doorBoss;

    [SerializeField] private int nEasy, nMedium, nHard;

    private void Start() {
        if(gm == null){
            gm = GameManager.instance;
        }
        gm.onSelectDoor.AddListener(SetBoss);
    }

    public void SetupBiome(){
        gm.possibleBiomes.Remove(gm.currentBiome);
        doorBiome = gm.SelectFrom(gm.possibleBiomes);

        GetComponent<SpriteRenderer>().sprite = doorBiome.biomeDoor;
    }

    public List<Boss> possibleBosses;
    public void SetupBoss(){
        List<Boss> easyBosses = new List<Boss>();
        easyBosses.AddRange(doorBiome.easyBosses);
        List<Boss> mediumBosses = new List<Boss>();
        mediumBosses.AddRange(doorBiome.mediumBosses);
        List<Boss> hardBosses = new List<Boss>();
        hardBosses.AddRange(doorBiome.hardBosses);

        possibleBosses = new List<Boss>();
        int i;

        if(nEasy > doorBiome.easyBosses.Count)
            nEasy = doorBiome.easyBosses.Count;

        if(nMedium > doorBiome.mediumBosses.Count)
            nMedium = doorBiome.mediumBosses.Count;

        if(nHard > doorBiome.hardBosses.Count)
            nHard = doorBiome.hardBosses.Count;

        for(i = 0; i < nEasy; i++){
            gm.AddToListFrom(possibleBosses, easyBosses);
        }

        for(i = 0; i < nMedium; i++){
            gm.AddToListFrom(possibleBosses, mediumBosses);
        }

        for(i = 0; i < nHard; i++){
            gm.AddToListFrom(possibleBosses, hardBosses);
        }

        doorBoss = gm.SelectFrom(possibleBosses);
    }

    public void SetBoss(){
        gm.currentBoss = doorBoss;
    }
}
