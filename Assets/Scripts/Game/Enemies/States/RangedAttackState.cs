using UnityEngine;

public class RangedAttackState : AttackState
{
    private Movement Movement => movement ? movement : core.GetCoreComponent<Movement>(ref movement);
    private Movement movement;

    protected D_RangedAttackState stateData;

    protected GameObject projectile;
    protected ProjectileObj projectileScript;

    public RangedAttackState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, Transform attackPosition, D_RangedAttackState stateData) : base(stateMachine, entity, animBoolName, attackPosition)
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

        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<ProjectileObj>();
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);
        projectileScript.ProjectileKnockBack(stateData.projectileAngel, stateData.projectileStrength, Movement.FacingDirection);
    }
}