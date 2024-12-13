using System.Collections.Generic;
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

    private bool isGravityOn;
    private bool hasHitGround;

    [SerializeField] private bool isDestroy = false;
    [SerializeField] private bool isThrough = false;
    [SerializeField] GameObject destroyInstance;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsDamage;
    [SerializeField]
    private Transform damagePosition;

    private HashSet<GameObject> damagedObjects = new HashSet<GameObject>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

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
    }

    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D[] damageHits = Physics2D.OverlapCircleAll(damagePosition.position, damageRadius, whatIsDamage);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            foreach (Collider2D damageHit in damageHits)
            {
                GameObject hitObject = damageHit.gameObject;

                if (!damagedObjects.Contains(hitObject))
                {
                    damagedObjects.Add(hitObject);

                    DamageReceiver target = damageHit.GetComponentInChildren<DamageReceiver>();
                    KnockBackReceiver targetKB = damageHit.GetComponentInChildren<KnockBackReceiver>();

                    if (target != null)
                    {
                        target.Damage(damage);
                    }

                    if (targetKB != null && !isThrough)
                    {
                        targetKB.KnockBack(angle, strength, direction);
                    }

                    if (!isThrough)
                    {
                        Destroy(gameObject);
                        return;
                    }
                }
            }

            if (groundHit)
            {
                hasHitGround = true;
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;

                if (isDestroy)
                {
                    Destroy(gameObject);
                }
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

    private void OnDestroy()
    {
        if (isDestroy)
        {
            Instantiate(destroyInstance, transform.position, Quaternion.identity);
        }
    }
}
