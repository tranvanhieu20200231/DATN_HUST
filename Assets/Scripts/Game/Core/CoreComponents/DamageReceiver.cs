﻿using TMPro;
using UnityEngine;

public class DamageReceiver : CoreComponent, IDamageable
{
    [SerializeField] private GameObject damageParticles;
    [SerializeField] private GameObject damagePopup;

    private Stats stats;
    private ParticleManager particleManager;

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        stats.Health.Decrease(amount);

        TextMeshProUGUI textPopup = damagePopup.GetComponentInChildren<TextMeshProUGUI>();
        textPopup.text = ((int)amount).ToString();

        particleManager.StartParticles(damagePopup);
        particleManager.StartParticlesWithRandomRotation(damageParticles);
    }

    protected override void Awake()
    {
        base.Awake();

        stats = core.GetCoreComponent<Stats>();
        particleManager = core.GetCoreComponent<ParticleManager>();
    }
}