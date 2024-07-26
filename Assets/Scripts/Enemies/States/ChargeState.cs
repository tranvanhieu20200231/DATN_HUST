using UnityEngine;

public class ChargeState : State
{
    private Movement Movement => movement ? movement : core.GetCoreComponent<Movement>(ref movement);
    private Movement movement;
    private CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent<CollisionSenses>(ref collisionSenses);
    private CollisionSenses collisionSenses;

    protected D_ChargeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isDectectingLedge;
    protected bool isDectectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChargeState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_ChargeState stateDate) : base(stateMachine, entity, animBoolName)
    {
        this.stateData = stateDate;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();

        if (CollisionSenses)
        {
            isDectectingLedge = CollisionSenses.LedgeVertical;
            isDectectingWall = CollisionSenses.WallFront;
        }

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;

        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
