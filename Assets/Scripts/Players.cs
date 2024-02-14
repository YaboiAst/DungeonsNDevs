using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Players : GenericEntity
{
    private GenericEntity boss;

    public int specialCooldownCounter;
    
    private void Start() {
        currentHealth = cInfo.maxHealth;

        gm = GameManager.instance;

        boss = gm.GetBoss();

        GetComponent<SpriteRenderer>().sprite = cInfo.art;
        //GetComponent<Animator>().runtimeAnimatorController = cInfo.animator;

    }

    public override bool isPlayer(){ return true; }

    public override void BasicAttack(int nAttacks = 1){
        boss.ChangeHealth(-cInfo.basicAttack);

        if(nAttacks == 1)
            return;
        
        for(int i = 1; i < nAttacks; i++){
            boss.ChangeHealth(-cInfo.basicAttack);
        }
    }

    public void ManageCooldown(){
        if(specialCooldownCounter > 0){
            specialCooldownCounter--;
        }
    }

    public override void SpecialAttack(){
        if(cInfo.specialAttack == ""){
            Debug.Log("Ataque especial");
            return;
        }

        specialCooldownCounter = cInfo.specialCooldown;
        Invoke(cInfo.specialAttack, 0.01f);
    }

    // SetShield(int shieldAmount)
    private void SetShield(){
        hasShield += cInfo.specialParams[0];
    }

    private void MultiAttacks(){
        BasicAttack((int) cInfo.specialParams[0]);
    }

    private void Stun(){
        boss.ChangeHealth(-cInfo.specialParams[0]);
        boss.isStuned = true;
    }

    private void HealAll(){
        List<Players> party = gm.GetParty();
        foreach(Players p in party){
            p.ChangeHealth(cInfo.specialParams[0]);
        }
    }
}
