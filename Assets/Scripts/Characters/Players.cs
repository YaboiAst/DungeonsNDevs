using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Players : GenericEntity
{
    [SerializeField] private Player playerInfo;
    private Monsters boss;

    [HideInInspector] public int specialCooldownCounter;

    [HideInInspector] public UnityEvent onPlayerDeath;

    public void SetPlayerInfo(Player playerInfo){
        this.playerInfo = playerInfo;
        characterSet?.Invoke();
    }

    internal override void SetupCharacter(){
        base.SetupCharacter();
        boss = tm.GetBoss();
        
        onPlayerDeath.AddListener(Die);
    }

    public override bool isPlayer(){ return true; }
    public override Character GetCharacter(){ return playerInfo; }
    public override Player GetPlayer(){ return playerInfo; }
    public override Boss GetBoss(){ return null; }

    public int GetThreatLevel(){ return playerInfo.threatLevel; }

    public override void ChangeHealth(float amount){
        base.ChangeHealth(amount);

        if(currentHealth <= 0){
            onPlayerDeath?.Invoke();
        }
    }

    private void Die(){
        tm.GetTurnOrder().Remove(this.gameObject);
        GetComponentInChildren<SpriteRenderer>().color = Color.gray;

        if(tm.GetTurnOrder().Count == 1){
            tm.GetTurnOrder().RemoveAt(0);
            gm.onChangeRoom?.Invoke("GameOver");
        }
    }

    public override void BasicAttack(){
        charAnimator.SetTrigger("Ataque");
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

        boss.onStun?.Invoke();
    }

    // HeallAll(float healAmount)
    private void HealAll(){
        List<Players> party = tm.GetParty();
        foreach(Players p in party){
            p.ChangeHealth(playerInfo.specialParams[0]);
        }
    }
}
