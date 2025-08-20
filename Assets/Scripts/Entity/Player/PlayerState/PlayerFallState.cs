using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(string animName) : base(animName)
    {
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        player.SetInvincible(true);
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        base.OnLeave(fsm);
        player.SetInvincible(false);
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        player.transform.position += new Vector3(0, -player.FloatDownSpeed * Time.deltaTime, 0);

        if (player.IsGrounded)
        {
            ChangeState<PlayerIdleState>(fsm);
        }
    }

    public static PlayerFallState Create(string animName)
    {
        return new PlayerFallState(animName);
    }
}