using System;
using UnityEngine;

[Serializable]
public class AttackPoiseDamage : AttackData
{
    [field: SerializeField] public float Amount { get; private set; }
}
