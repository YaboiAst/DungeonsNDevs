using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatEvent : UnityEvent<float>{}

public abstract class GenericEntity : MonoBehaviour
{
    internal Character cInfo;
    internal TurnManager tm;
    internal GameManager gm;
    internal Animator charAnimator;

    internal float currentHealth;
    internal float hasShield = 0;
    internal bool isStuned = false;

    [HideInInspector] public UnityEvent characterSet;

    [HideInInspector] public FloatEvent onHealthChange = new FloatEvent();
    [HideInInspector] public UnityEvent onShield;
    [HideInInspector] public UnityEvent onStun;


    // GENERIC FUNCTIONS ------------------------------------------
    public abstract bool isPlayer();
    public abstract void BasicAttack();

    public abstract Character GetCharacter();
    public abstract Player GetPlayer();
    public abstract Boss GetBoss();

    public GenericEntity GetGenericEntity(){
        return this;
    }

    private void Awake() {
        characterSet.AddListener(SetupCharacter);
    }

    internal virtual void SetupCharacter(){
        gm = GameManager.instance;
        tm = TurnManager.instance;

        cInfo = GetCharacter();
        GetComponentInChildren<SpriteRenderer>().sprite = cInfo.art;
        charAnimator = GetComponentInChildren<Animator>();
        charAnimator.runtimeAnimatorController = cInfo.combatAnimator;

        currentHealth = cInfo.maxHealth;
        onHealthChange?.Invoke(0);
    }

    public void ChangeHealth(float amount){
        if(hasShield > 0){
            hasShield -= 1f;
            // Invoke em alguma animação?
            return;
        }

        if(amount > 0 && currentHealth == cInfo.maxHealth){
            return;
        }

        currentHealth += amount;

        if(currentHealth < 0) currentHealth = 0;
        if(currentHealth > cInfo.maxHealth) currentHealth = cInfo.maxHealth;

        onHealthChange?.Invoke(amount);
    }
    
    public string GetEntityName(){
        return cInfo.characterName;
    }
}
