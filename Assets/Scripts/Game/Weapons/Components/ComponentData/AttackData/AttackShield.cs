using System;
using UnityEngine;

[Serializable]
public class AttackShield : AttackData
{
    [field: SerializeField] public float ReductionRate { get; private set; }
}
