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

    public override void BasicAttack(){
        boss.ChangeHealth(-cInfo.basicAttack);

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

    [HideInInspector] private int qAttacks;
    // MultiAttacks(int qAttacks)
    private void MultiAttacks(){
        qAttacks = (int) cInfo.specialParams[0];
        BasicAttack();
    }

    // Stun(float stunDamage)
    private void Stun(){
        boss.ChangeHealth(-cInfo.specialParams[0]);
        boss.isStuned = true;
    }

    // HeallAll(float healAmount)
    private void HealAll(){
        List<Players> party = gm.GetParty();
        foreach(Players p in party){
            p.ChangeHealth(cInfo.specialParams[0]);
        }
    }
}
