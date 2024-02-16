using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Player")]
public class Player : Character
{
    public int threatLevel;

    public string specialAttack;
    public float[] specialParams;
    public int specialCooldown;

    [Space(10)]

    public Sprite basicAttackButton;
    public string basicAttackName;
    public string basicAttackDescription;
    [Space(5)]
    public Sprite specialAttackButton;
    public string specialAttackName;
    public string specialAttackDescription;
}