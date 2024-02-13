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

    [HideInInspector] public UnityEvent onHealthChange; 

    // GENERIC FUNCTIONS ------------------------------------------
    public abstract bool isPlayer();

    public abstract void BasicAttack(int nAttacks = 1);
    public abstract void SpecialAttack();

    public void ChangeHealth(float amount){
        currentHealth += amount;

        if(currentHealth < 0) currentHealth = 0;
        if(currentHealth > cInfo.maxHealth) currentHealth = cInfo.maxHealth;

        onHealthChange?.Invoke();
    }
    
    public string GetEntityName(){
        return cInfo.characterName;
    }

    

}
