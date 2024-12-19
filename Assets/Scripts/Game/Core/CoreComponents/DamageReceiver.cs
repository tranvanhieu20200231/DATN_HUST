using UnityEngine;

public class DamageReceiver : CoreComponent, IDamageable
{
    [SerializeField] private GameObject damageParticles;
    [SerializeField] private GameObject reductionParticle;

    private float Reduction = 0;

    private Stats stats;
    private ParticleManager particleManager;
    private PlayerSoundManager playerSoundManager;

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");

        if (LayerMask.LayerToName(gameObject.layer) == "Player")
        {
            if (Reduction == 0)
            {
                playerSoundManager = GetComponent<PlayerSoundManager>();

                if (playerSoundManager != null)
                {
                    playerSoundManager.PlaySound("Hurt");
                }

                stats.Health.Decrease(amount);
            }
            else if (Reduction < 100)
            {
                playerSoundManager = GetComponent<PlayerSoundManager>();

                if (playerSoundManager != null)
                {
                    playerSoundManager.PlaySound("ShieldBlock");
                }

                stats.Health.Decrease(amount * (100 - Reduction) / 100);
                particleManager.StartParticlesWithRandomRotation(reductionParticle);
            }
            else
            {
                stats.Health.Increase(amount * (Reduction - 100) / 100);
            }
        }
        else
        {
            stats.Health.Decrease(amount);
            particleManager.StartParticlesWithRandomRotation(damageParticles);
        }
    }

    public void GetReduction(float reduction) => Reduction = reduction;

    protected override void Awake()
    {
        base.Awake();

        stats = core.GetCoreComponent<Stats>();
        particleManager = core.GetCoreComponent<ParticleManager>();
    }
}
