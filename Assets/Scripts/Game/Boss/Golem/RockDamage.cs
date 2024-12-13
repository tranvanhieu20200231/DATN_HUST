using UnityEngine;

public class RockDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsDamage;
    [SerializeField]
    private float damageRadius;

    [SerializeField] GameObject destroyInstance;

    private void FixedUpdate()
    {
        Collider2D damageHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsDamage);
        Collider2D groundHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsGround);

        if (damageHit != null)
        {
            DamageReceiver target = damageHit.GetComponentInChildren<DamageReceiver>();

            if (target != null)
            {
                target.Damage(damage);
                Destroy(gameObject);
                return;
            }
        }

        if (groundHit != null)
        {
            Destroy(gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

    private void OnDestroy()
    {
        Instantiate(destroyInstance, transform.position, Quaternion.identity);
    }
}
