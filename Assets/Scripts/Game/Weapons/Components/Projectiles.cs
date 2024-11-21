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
            GameObject projectile = Instantiate(currentAttackData.Projectile,
                transform.position + new Vector3(currentAttackData.firePos.x * movement.FacingDirection, currentAttackData.firePos.y, 0),
                transform.rotation);

            projectileObjScript = projectile.GetComponent<ProjectileObj>();

            projectileObjScript.FireProjectile(currentAttackData.Speed * multiplier
                , currentAttackData.TravelDistance * multiplier
                , (currentAttackData.DamageData.Amount + PlayerData.attack / 5) * multiplier);

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

    private void OnDrawGizmos()
    {
        if (currentAttackData == null)
            return;

        foreach (var item in data.AttackData)
        {
            if (!item.Debug)
                continue;

            Gizmos.color = Color.red;
            Vector2 firePosWorld = transform.position + (Vector3)currentAttackData.firePos;
            Gizmos.DrawWireSphere(firePosWorld, 0.1f);
        }
    }
}


