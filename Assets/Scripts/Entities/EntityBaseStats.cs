using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBaseStats : ScriptableObject
{
    [SerializeField] private Stats stats;
    public Stats baseStats => stats;
}

public struct Stats
{
    public int Health;
    public int AttackPower;
    public int MovementSpeed;
}
