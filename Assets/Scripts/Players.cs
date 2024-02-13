using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Players : GenericEntity
{
    private GenericEntity boss;
    
    private void Start() {
        currentHealth = cInfo.maxHealth;

        gm = GameManager.instance;
        boss = gm.GetBoss();

        GetComponent<SpriteRenderer>().sprite = cInfo.art;
        //GetComponent<Animator>().runtimeAnimatorController = cInfo.animator;

    }

    public override void BasicAttack(int nAttacks = 1){
        boss.ChangeHealth(-cInfo.basicAttack);

        if(nAttacks == 1)
            return;
        
        for(int i = 1; i < nAttacks; i++){
            boss.ChangeHealth(-cInfo.basicAttack);
        }
    }

    public override void SpecialAttack(){
        if(cInfo.specialAttack == ""){
            Debug.Log("Ataque especial");
            return;
        }

        cInfo.isSpecialUp = false;
        cInfo.SetCooldownCounter();
        Invoke(cInfo.specialAttack, 0f);
    }

    public void ManageCooldown(){
        if(!cInfo.isSpecialUp){
            cInfo.UpdateCooldown();
        }
    }

    public override bool isPlayer(){ return true; }
}
