using System;
using UnityEngine;

[Serializable]
public class AttackInputHold : AttackData
{
    [field: SerializeField] public float Multiplier { get; private set; }
}
