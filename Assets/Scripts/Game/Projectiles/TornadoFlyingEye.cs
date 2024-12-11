using UnityEngine;

public class TornadoFlyingEye : MonoBehaviour
{
    private float speed;
    private float travelDistance;
    private float damage;

    private Vector2 angle;
    private float strength;
    private int direction;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;
    private Animator anim;

    private bool hasHitGround;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsDamage;
    [SerializeField]
    private Transform damagePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.velocity = transform.right * speed;
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsDamage);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

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

                rb.isKinematic = true;
                rb.velocity = Vector2.zero;

                anim.SetTrigger("isDestroy");
            }

            if (groundHit)
            {
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;

                anim.SetTrigger("isDestroy");
            }
        }
    }

    public void FireProjectile(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
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
