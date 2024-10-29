using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Equipable,
    Consumable,
    Resource,
    Structure
}

public enum ConsumableType
{
    Health,
    Hunger
}

[Serializable]
public class ItemDataCousumable
{
    public ConsumableType type;
    public float value;
}
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataCousumable[] consumables;

    [Header("Getable")]
    public bool getable;
}
