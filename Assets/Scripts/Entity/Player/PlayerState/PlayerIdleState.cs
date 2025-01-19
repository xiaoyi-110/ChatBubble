

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(string animName) : base(animName)
    {
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        fsm.Data = typeof(PlayerIdleState);
    
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        base.OnLeave(fsm);
        
    }
    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        if(m_HasChanged)return;

        m_Player.Attack();

        if(m_Player.isTryJump)
        {
            fsm.ChangeState<PlayerJumpState>();
        }
    }
    public static PlayerIdleState Create(string animName)
    {
        return new PlayerIdleState(animName);
    }
}