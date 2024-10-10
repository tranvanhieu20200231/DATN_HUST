using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_MeleeAttackState meleeAttackState { get; private set; }
    public E2_LookForPlayerState lookForPlayerState { get; private set; }
    public E2_StunState stunState { get; private set; }
    public E2_DeadState deadState { get; private set; }
    public E2_DodgeState dodgeState { get; private set; }
    public E2_RangedAttackState rangedAttackState { get; private set; }

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_MeleeAttackState meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayerData lookForPlayerData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    public D_RangedAttackState rangedAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;

    public override void Awake()
    {
        base.Awake();

        moveState = new E2_MoveState(stateMachine, this, "move", moveStateData, this);
        idleState = new E2_IdleState(stateMachine, this, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(stateMachine, this, "playerDetected", playerDetectedStateData, this);
        meleeAttackState = new E2_MeleeAttackState(stateMachine, this, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(stateMachine, this, "lookForPlayer", lookForPlayerData, this);
        stunState = new E2_StunState(stateMachine, this, "stun", stunStateData, this);
        deadState = new E2_DeadState(stateMachine, this, "dead", deadStateData, this);
        dodgeState = new E2_DodgeState(stateMachine, this, "dodge", dodgeStateData, this);
        rangedAttackState = new E2_RangedAttackState(stateMachine, this, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);

        stats.Poise.OnCurrentValueZero += HandlePoiseZero;
    }

    private void HandlePoiseZero()
    {
        stateMachine.ChangeState(stunState);
    }

    private void Start()
    {
        stateMachine.Initialize(moveState);
    }

    private void OnDestroy()
    {
        stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
    }

    //public override void Damage(AttackDetails attackDetails)
    //{
    //    base.Damage(attackDetails);

    //    if (isDead)
    //    {
    //        stateMachine.ChangeState(deadState);
    //    }
    //    else if (isStunned && stateMachine.currentState != stunState)
    //    {
    //        stateMachine.ChangeState(stunState);
    //    }
    //    else if (CheckPlayerInMinAgroRange())
    //    {
    //        stateMachine.ChangeState(rangedAttackState);
    //    }
    //    else if (!CheckPlayerInMinAgroRange())
    //    {
    //        lookForPlayerState.SetTurnImmediately(true);
    //        stateMachine.ChangeState(lookForPlayerState);
    //    }
    //}

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
