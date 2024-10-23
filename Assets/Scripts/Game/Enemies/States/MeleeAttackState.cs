using UnityEngine;

public class MeleeAttackState : AttackState
{
    private Movement Movement => movement ? movement : core.GetCoreComponent<Movement>(ref movement);
    private Movement movement;

    protected D_MeleeAttackState stateData;

    public MeleeAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(stateMachine, entity, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObject = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObject)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(stateData.attackDamage);
            }

            IKnockBackable knockbackable = collider.GetComponent<IKnockBackable>();

            if (knockbackable != null)
            {
                knockbackable.KnockBack(stateData.knockbackAngle, stateData.knockbackStrength, Movement.FacingDirection);
            }
        }
    }
}
