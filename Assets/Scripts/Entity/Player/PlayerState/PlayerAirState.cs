
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(string animBoolName) : base(animBoolName)
    {
    }

    public override void OnUpdate(FSM<Player> fsm)
    {

        base.OnUpdate(fsm);
        if (m_IsChanged) return;

        if(m_Player.IsTryJumpAtCoyoteTime)
        {
            ChangeState<PlayerJumpState>(fsm);
        } 
        else if(m_Player.IsGroundDetected())
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }

    public static PlayerAirState Create()
    {
        PlayerAirState playerAirState = new PlayerAirState("JumpFall");
        return playerAirState;
    }
}