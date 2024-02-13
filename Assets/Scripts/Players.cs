using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Players : GenericEntity
{
    [Header("Visual")]
    [SerializeField] private Image iconUI;
    [SerializeField] private TextMeshProUGUI nameUI;
    
    [SerializeField] private Image healthUI;
    [SerializeField] private TextMeshProUGUI healthIndicatorUI;

    // ============================================================= //

    private GenericEntity boss;

    private void Start() {  
        gm = GameManager.instance;
        boss = gm.GetBoss();

        GetComponent<SpriteRenderer>().sprite = cInfo.art;
        //GetComponent<Animator>().runtimeAnimatorController = cInfo.animator;

        currentHealth = cInfo.maxHealth;

        iconUI.sprite = cInfo.classIcon;
        nameUI.text = cInfo.characterName;
        
        healthIndicatorUI.text = currentHealth.ToString() + "/" + cInfo.maxHealth.ToString();
        healthUI.fillAmount = currentHealth/cInfo.maxHealth;
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
