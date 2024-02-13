using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Players : MonoBehaviour
{
    [Header("Character Info")]
    [SerializeField] private Character cInfo;

    private float currentHealth;

    [Header("Visual")]
    [SerializeField] private Image iconUI;
    [SerializeField] private TextMeshProUGUI nameUI;
    
    [SerializeField] private Image healthUI;
    [SerializeField] private TextMeshProUGUI healthIndicatorUI;

    private void Start() {
        GetComponent<SpriteRenderer>().sprite = cInfo.art;
        //GetComponent<Animator>().runtimeAnimatorController = cInfo.animator;

        currentHealth = cInfo.maxHealth;

        iconUI.sprite = cInfo.classIcon;
        nameUI.text = cInfo.characterName;
        
        healthIndicatorUI.text = currentHealth.ToString() + "/" + cInfo.maxHealth.ToString();
        healthUI.fillAmount = currentHealth/cInfo.maxHealth;
    }

    private void BasicAttack(int nAttacks = 1){
        Debug.Log("Dealing " + cInfo.basicAttack + " to the monster " + nAttacks + " times");

        if(nAttacks == 1)
            return;
        
        for(int i = 1; i < nAttacks; i++){
            Debug.Log("Dealing " + cInfo.basicAttack + " to the monster...");
        }
    }

    private void SpecialAttack(){
        Invoke(cInfo.specialAttack, 0f);
    }

    private void ChangeHealth(float amount){
        currentHealth += amount;

        if(currentHealth < 0) currentHealth = 0;
        if(currentHealth > cInfo.maxHealth) currentHealth = cInfo.maxHealth;
    }
}
