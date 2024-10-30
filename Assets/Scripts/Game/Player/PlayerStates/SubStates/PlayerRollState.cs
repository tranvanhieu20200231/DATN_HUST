using UnityEngine;

public class PlayerRollState : PlayerAbilityState
{
    public bool CanRoll { get; private set; }

    private float lastRollTime;
    private float rollStartTime;

    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanRoll = false;
        player.InputHandler.UseRollInput();

        rollStartTime = Time.time;

        Movement?.SetVelocityX(playerData.rollVelocity * Movement.FacingDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= rollStartTime + playerData.rollTime)
        {
            isAbilityDone = true;
            lastRollTime = Time.time;
            Movement?.SetVelocityX(0);
        }
    }

    public bool CheckIfCanRoll()
    {
        return CanRoll && Time.time >= lastRollTime + playerData.rollCooldown;
    }

    public void ResetCanRoll() => CanRoll = true;
}
