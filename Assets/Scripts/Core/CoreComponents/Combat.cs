using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] private GameObject damageParticles;

    private Movement Movement => movement ? movement : core.GetCoreComponent<Movement>(ref movement);
    private Movement movement;
    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent<CollisionSenses>(ref collisionSenses);
    private CollisionSenses collisionSenses;
    private Stats Stats => stats ? stats : core.GetCoreComponent<Stats>(ref stats);
    private Stats stats;

    private ParticleManager ParticleManager => particleManager ? particleManager : core.GetCoreComponent<ParticleManager>(ref particleManager);
    private ParticleManager particleManager;

    [SerializeField] private float maxKnockbackTime = 0.3f;

    private bool isKnockbackActive;
    private float knockbackStartTime;

    public override void LogicUpdate()
    {
        CheckKnockback();
    }

    public void Damage(float amount)
    {
        Debug.Log(core.transform.parent.name + " Damaged!");
        Stats?.DecreaseHealth(amount);
        ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        isKnockbackActive = true;
        knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if (isKnockbackActive && ((Movement?.CurrentVelocity.y <= 0.01f && (CollisionSenses.Ground) || Time.time >= knockbackStartTime + maxKnockbackTime)))
        {
            isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}
