

using System.Numerics;

public class PlayerSprintState : PlayerState
{
    public PlayerSprintState(string animBoolName) : base(animBoolName)
    {
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        m_Player.ResetSprintTimer();
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        base.OnLeave(fsm);
    }

    public override void OnUpdate(FSM<Player> fsm)
    {  
        base.OnUpdate(fsm);
        if (m_IsChanged) return;

        m_Player.SetVelocity(m_Player.FacingDirection * m_Player.SprintSpeed, 0);

        if(m_IsTriggerCalled)
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }

    public static PlayerSprintState Create()
    {
        return new PlayerSprintState("Sprint");
    }
}