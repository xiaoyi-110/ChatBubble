
using UnityEngine.UIElements;

public class PlayerState : FSMState<Player>
{


    protected string m_AnimName;
    protected Player m_Player;
    protected bool m_HasChanged;

    public PlayerState(string animName)
    {
        m_AnimName = animName;    
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        m_Player = fsm.Owner;
        m_Player.m_Animator.SetBool(m_AnimName, true);
        m_HasChanged = false;
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        base.OnLeave(fsm);
        m_Player.m_Animator.SetBool(m_AnimName, false);
        m_HasChanged = true;
    }
    
    
    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        if (m_HasChanged)return;

        if(m_Player.InvincibleTimer > 0)
        {
            m_HasChanged = true;
            fsm.ChangeState<PlayerAttackedState>();
        }
    }


}