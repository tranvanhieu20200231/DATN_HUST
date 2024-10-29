using System;
using UnityEngine;

[Serializable]
public class AttackProjectile : AttackData
{
    [field: SerializeField] private GameObject projectile;
    public GameObject Projectile { get => projectile; private set => projectile = value; }

    [field: SerializeField] public AttackDamage DamageData { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float TravelDistance { get; private set; }

    [field: SerializeField] public AttackKnockBack KnockBackData { get; private set; }
}
