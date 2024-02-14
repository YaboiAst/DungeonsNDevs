using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Player")]
public class Character : ScriptableObject
{
    public string characterName;

    [Space(10)]

    public float maxHealth;
    public float basicAttack;

    [Space(10)]

    public string specialAttack;
    public float[] specialParams;
    public int specialCooldown;
 
    [Space(10)]

    public Sprite art;
    public Sprite classIcon;
    public AnimatorController animator;

    [HideInInspector] public UnityEvent onTakeDamage = new UnityEvent();
    [HideInInspector] public UnityEvent onDealDamage = new UnityEvent();
    [HideInInspector] public UnityEvent onSkillUse = new UnityEvent();
}