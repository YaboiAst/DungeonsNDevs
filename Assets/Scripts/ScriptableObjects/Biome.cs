using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "New Biome", menuName = "Biome")]
public class Biome : ScriptableObject{
    public enum Biomes{CITY, FOREST, OCEAN, VULCAN, CAVE};
    public Biomes biome;

    public string biomeScene;
    public Sprite biomeDoor;

    [Space(10)]

    [BoxGroup("Monsters")]
    [Label("Easy")]   public List<Boss> easyBosses;

    [BoxGroup("Monsters")]
    [Label("Medium")] public List<Boss> mediumBosses;

    [BoxGroup("Monsters")]
    [Label("Hard")]   public List<Boss> hardBosses;
}
