using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class Character : ScriptableObject
{
    public string characterName;

    [Space(10)]

    public float maxHealth;
    public float basicAttack;
 
    [Space(10)]

    public Sprite art;
    public AnimatorController animator;
}