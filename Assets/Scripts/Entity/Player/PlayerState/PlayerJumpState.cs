using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(string animBoolName) : base(animBoolName)
    {
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        m_Player.SetVelocity(m_Rb.velocity.x, m_Player.JumpForce);
        m_Player.ClearCoyoteTimer();
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        if(m_IsChanged) return;

        if(m_Rb.velocity.y < 0)
        {
            ChangeState<PlayerAirState>(fsm);
        }
    }

    public static PlayerJumpState Create()
    {
        PlayerJumpState state = new PlayerJumpState("JumpFall");
        return state;
    }
    
}