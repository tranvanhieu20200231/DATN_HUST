using UnityEngine;

public class Projectiles : WeaponComponent<ProjectileData, AttackProjectile>
{
    private Movement movement;

    private InputHold inputHold;

    protected ProjectileObj projectileObjScript;

    private float multiplier;

    private void HanlderInstanceProjectile()
    {
        if (inputHold != null)
        {
            multiplier = inputHold.GetMultiplier();
        }
        else
        {
            multiplier = 1;
        }

        if (currentAttackData.Projectile != null)
        {
            GameObject projectile = Instantiate(currentAttackData.Projectile, transform.position, transform.rotation);
            projectileObjScript = projectile.GetComponent<ProjectileObj>();
            projectileObjScript.FireProjectile(currentAttackData.Speed * multiplier
                , currentAttackData.TravelDistance * multiplier
                , currentAttackData.DamageData.Amount * multiplier);
            if (currentAttackData.KnockBackData != null)
            {
                projectileObjScript.ProjectileKnockBack(currentAttackData.KnockBackData.Angle * multiplier
                    , currentAttackData.KnockBackData.Strength * multiplier
                    , movement.FacingDirection);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        movement = Core.GetCoreComponent<Movement>();

        inputHold = GetComponent<InputHold>();
    }

    protected override void Start()
    {
        base.Start();

        eventHandler.OnInstanceProjectile += HanlderInstanceProjectile;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        eventHandler.OnInstanceProjectile -= HanlderInstanceProjectile;
    }
}


