using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GenericEntity : MonoBehaviour
{
    [Header("Character Info")]
    [SerializeField] internal Character cInfo;
    internal GameManager gm;

    internal float currentHealth;
    internal float hasShield = 0;
    internal bool isStuned = false;

    [HideInInspector] public UnityEvent onHealthChange; 

    // GENERIC FUNCTIONS ------------------------------------------
    public abstract bool isPlayer();

    public abstract void BasicAttack();
    public abstract void SpecialAttack();

    public Character GetCharacter(){ return cInfo; }

    public void ChangeHealth(float amount){
        if(hasShield > 0){
            hasShield -= 1f;
            // Invoke em alguma animação?
            return;
        }

        currentHealth += amount;

        if(currentHealth < 0) currentHealth = 0;
        if(currentHealth > cInfo.maxHealth) currentHealth = cInfo.maxHealth;

        onHealthChange?.Invoke();
    }
    
    public string GetEntityName(){
        return cInfo.characterName;
    }

    

}
