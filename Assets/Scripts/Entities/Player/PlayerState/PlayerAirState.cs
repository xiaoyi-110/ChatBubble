using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, string _animBoolName) : base(_player, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.bufferCoyoteTimer -= Time.deltaTime;

        if(player.IsAtCoyoteTime && player.IsTryJump)
        {
            player.stateMachine.ChangeState(typeof(PlayerJumpState));
        } 
        else if(player.IsGroundDetected())
        {
            player.stateMachine.ChangeState(typeof(PlayerIdleState));
        }
    }
}