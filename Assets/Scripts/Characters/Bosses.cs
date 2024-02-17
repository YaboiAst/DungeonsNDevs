using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosses : GenericEntity
{
    [SerializeField] private Boss bossInfo;

    private List<Players> party;
    List<Players> decisionList;
    private Players target;


    internal override void Start() {
        base.Start();
        UnityEngine.Random.InitState((int) long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")));
        party = gm.GetParty();

        decisionList = new List<Players>();
        foreach(Players player in party){
            int size = player.GetThreatLevel();
            for(int i = 0; i < size; i++){
                decisionList.Add(player);
            }
        }
    }

    public override bool isPlayer() { return false; }
    public override Character GetCharacter(){ return bossInfo; }
    public override Boss GetBoss(){ return bossInfo; }
    public override Player GetPlayer(){ return null; }

    public void TakeAction(){
        int rand = UnityEngine.Random.Range(0, decisionList.Count);
        target = decisionList.ToArray()[rand];

        Invoke("BasicAttack", 2f);
    }

    public override void BasicAttack(){
        target.ChangeHealth(-bossInfo.basicAttack);
        gm.Next();
    }
}
