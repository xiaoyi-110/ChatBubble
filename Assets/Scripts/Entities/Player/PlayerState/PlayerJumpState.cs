using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, string _animBoolName) : base(_player, _animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        player.bufferCoyoteTimer = -1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(rb.velocity.y < 0)
        {
            player.stateMachine.ChangeState(typeof(PlayerAirState));
        }
    }
}