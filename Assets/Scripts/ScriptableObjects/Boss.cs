using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Boss")]
public class Boss : Character
{
    public enum biomes{CITY, FOREST, OCEAN, VULCAN, CAVE};

    public biomes nativeBiome;
}