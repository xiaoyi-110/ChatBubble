
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(string animName) : base(animName)
    {
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        m_Player.IsInvincible = true;
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        base.OnLeave(fsm);
        m_Player.IsInvincible = false;
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        m_Player.transform.position = new Vector3(m_Player.transform.position.x, m_Player.transform.position.y - m_Player.UpDownSpeed * Time.deltaTime, m_Player.transform.position.z);

        if(m_Player.IsGrounded)
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }

    public static PlayerFallState Create(string animName)
    {
        return new PlayerFallState(animName);
    }
}