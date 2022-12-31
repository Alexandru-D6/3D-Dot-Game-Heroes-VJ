using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LootObj : ScriptableObject
{
    public GameObject prefav;
    public string lootName;
    public int dropChance;
    public LootObj(string lootName, int dropChance)
    {
        this.lootName = lootName;
        this.dropChance = dropChance;
    }
}
