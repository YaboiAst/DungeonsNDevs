using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosses : GenericEntity
{
    [SerializeField] private Boss bossInfo;

    List<Players> decisionList;
    private Players target;

    internal override void SetupCharacter(){
        base.SetupCharacter();
        List<Players> party = new List<Players>();
        party.AddRange(tm.GetParty());
        int size = party.Count;

        decisionList = new List<Players>();
        for(int i = 0; i < size; i++){
            gm.AddToListFrom(decisionList, party);
        }
    }

    public void SetBossInfo(Boss bossInfo){
        this.bossInfo = bossInfo;
        characterSet?.Invoke();
    }

    public override bool isPlayer() { return false; }
    public override Character GetCharacter(){ return bossInfo; }
    public override Boss GetBoss(){ return bossInfo; }
    public override Player GetPlayer(){ return null; }

    public void TakeAction(){
        target = gm.SelectFrom(decisionList, false);

        Invoke("BasicAttack", 2f);
    }

    public override void BasicAttack(){
        target.ChangeHealth(-bossInfo.basicAttack);
        tm.Next();
    }
}
