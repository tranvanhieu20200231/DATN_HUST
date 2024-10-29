using UnityEngine;

public class ProjectileObj : MonoBehaviour
{
    private float speed;
    private float travelDistance;
    private float damage;

    private Vector2 angle;
    private float strength;
    private int direction;

    private float xStartPos;

    [SerializeField]
    private float gravity;
    [SerializeField]
    private float damageRadius;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isGravityOn;
    private bool hasHitGround;
    private bool isFly;

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

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        isFly = true;
        isGravityOn = false;

        xStartPos = transform.position.x;
    }

    private void Update()
    {
        if (!hasHitGround)
        {
            if (isGravityOn)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        anim.SetBool("fly", isFly);
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

                Destroy(gameObject);
            }

            if (groundHit)
            {
                isFly = false;
                hasHitGround = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }

            if (Mathf.Abs(xStartPos - transform.position.x) >= travelDistance && !isGravityOn)
            {
                isGravityOn = true;
                rb.gravityScale = gravity;
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
}
