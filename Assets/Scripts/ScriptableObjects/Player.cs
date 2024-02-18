using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Player")]
public class Player : Character
{
    public AnimatorController walkingController;
    public int threatLevel;

    [Space(10)]

    public string specialAttack;
    public float[] specialParams;
    public int specialCooldown;

    [Space(10)]

    public Sprite basicAttackButton;
    public string basicAttackName;
    [ResizableTextArea]
    public string basicAttackDescription;
    [Space(5)]
    public Sprite specialAttackButton;
    public string specialAttackName;
    [ResizableTextArea]
    public string specialAttackDescription;
}