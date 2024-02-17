using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Boss")]
public class Boss : Character
{
    public Biome.Biomes nativeBiome;

    [ResizableTextArea]
    public string bossDescription;

    [ResizableTextArea]
    public string bossCommentary;
}