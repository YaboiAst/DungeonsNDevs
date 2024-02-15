using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Players : GenericEntity
{
    [SerializeField] private Player playerInfo;
    private Bosses boss;

    [HideInInspector] public int specialCooldownCounter;
    
    internal override void Start() {
        base.Start();
        boss = gm.GetBoss();
    }

    public override bool isPlayer(){ return true; }
    public override Character GetCharacter(){ return playerInfo; }
    public override Player GetPlayer(){ return playerInfo; }
    public override Boss GetBoss(){ return null; }

    public int GetThreatLevel(){ return playerInfo.threatLevel; }

    public override void BasicAttack(){
        boss.ChangeHealth(-playerInfo.basicAttack);

        if(qAttacks > 1){
            qAttacks--;
            Invoke("BasicAttack", 0.5f);
        }
    }

    public void ManageCooldown(){
        if(specialCooldownCounter > 0){
            specialCooldownCounter--;
        }
    }

    public void SpecialAttack(){
        if(playerInfo.specialAttack == ""){
            Debug.Log("Ataque especial");
            return;
        }

        specialCooldownCounter = playerInfo.specialCooldown;
        Invoke(playerInfo.specialAttack, 0.01f);
    }

    // SetShield(int shieldAmount)
    private void SetShield(){
        hasShield += playerInfo.specialParams[0];
    }

    [HideInInspector] private int qAttacks;
    // MultiAttacks(int qAttacks)
    private void MultiAttacks(){
        qAttacks = (int) playerInfo.specialParams[0];
        BasicAttack();
    }

    // Stun(float stunDamage)
    private void Stun(){
        boss.ChangeHealth(-playerInfo.specialParams[0]);
        boss.isStuned = true;

        onStun?.Invoke();
    }

    // HeallAll(float healAmount)
    private void HealAll(){
        List<Players> party = gm.GetParty();
        foreach(Players p in party){
            p.ChangeHealth(playerInfo.specialParams[0]);
        }
    }
}
