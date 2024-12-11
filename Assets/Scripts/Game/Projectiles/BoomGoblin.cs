using UnityEngine;

public class BoomGoblin : MonoBehaviour
{
    private float speed;
    private float damage;

    private Vector2 angle;
    private float strength;
    private int direction;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private bool hasHitTarget;

    [SerializeField]
    private LayerMask whatIsDamage;
    [SerializeField]
    private Transform damagePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Vector2 direction = transform.right + new Vector3(0, 1, 0);
        direction.Normalize();

        rb.velocity = direction * speed;


        hasHitTarget = false;
    }

    private void FixedUpdate()
    {
        if (!hasHitTarget)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsDamage);

            if (damageHit)
            {
                DamageReceiver target = damageHit.GetComponentInChildren<DamageReceiver>();
                KnockBackReceiver targetKB = damageHit.GetComponentInChildren<KnockBackReceiver>();

                if (target != null)
                {
                    target.Damage(damage);
                }

                if (targetKB != null)
                {
                    targetKB.KnockBack(angle, strength, direction);
                }

                hasHitTarget = true;
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                anim.SetTrigger("isDestroy");
            }
        }
    }

    public void FireProjectile(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    public void ProjectileKnockBack(Vector2 angle, float strength, int direction)
    {
        this.angle = angle;
        this.strength = strength;
        this.direction = direction;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }

    public void isDestroy()
    {
        Destroy(gameObject);
    }
}
