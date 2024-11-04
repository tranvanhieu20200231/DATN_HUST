using UnityEngine;

public class Entity : MonoBehaviour
{
    private Movement Movement => movement ? movement : Core.GetCoreComponent<Movement>(ref movement);
    private Movement movement;
    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : Core.GetCoreComponent<CollisionSenses>(ref collisionSenses);
    private CollisionSenses collisionSenses;

    public FiniteStateMachine stateMachine;

    public D_Entity entityData;

    public int lastDamageDirection { get; private set; }
    public Animator anim { get; private set; }
    public AnimationToStatemachine atsm { get; private set; }
    public Core Core { get; private set; }

    [SerializeField]
    private Transform playerCheck;

    private float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;

    protected Stats stats;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        stats = Core.GetCoreComponent<Stats>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStatemachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        Core.LogicUpdate();

        stateMachine.currentState.LogicUpdate();

        anim.SetFloat("yVelocity", Movement.RB.velocity.y);

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(Movement.RB.velocity.x, velocity);
        Movement.RB.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    //public virtual void Damage(AttackDetails attackDetails)
    //{
    //    lastDamageTime = Time.time;

    //    currentHealth -= attackDetails.damageAmount;
    //    currentStunResistance -= attackDetails.stunDamageAmount;

    //    DamageHop(entityData.damageHopSpeed);

    //    Instantiate(entityData.hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

    //    if (attackDetails.position.x > transform.position.x)
    //    {
    //        lastDamageDirection = -1;
    //    }
    //    else
    //    {
    //        lastDamageDirection = 1;
    //    }

    //    if (currentStunResistance <= 0)
    //    {
    //        isStunned = true;
    //    }

    //    if (currentHealth <= 0)
    //    {
    //        isDead = true;
    //    }
    //}

    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.DrawLine(CollisionSenses.WallCheck.position, CollisionSenses.WallCheck.position + (Vector3)(Vector2.right * Movement?.FacingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(CollisionSenses.LedgeCheckVertical.position, CollisionSenses.LedgeCheckVertical.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

            Gizmos.DrawWireSphere(CollisionSenses.WallCheck.position, entityData.wallCheckRadius);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
        }
    }
}
