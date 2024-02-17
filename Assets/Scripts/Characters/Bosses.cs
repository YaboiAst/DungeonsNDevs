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

    internal override void SetupCharacter(){
        base.SetupCharacter();
        party = tm.GetParty();

        decisionList = new List<Players>();
        foreach(Players player in party){
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
        target = gm.SelectFrom(decisionList);

        Invoke("BasicAttack", 2f);
    }

    public override void BasicAttack(){
        target.ChangeHealth(-bossInfo.basicAttack);
        tm.Next();
    }
}
