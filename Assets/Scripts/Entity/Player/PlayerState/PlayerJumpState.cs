using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(string animName) : base(animName)
    {
    }


    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        AudioManager.Instance.Play("playerJump");
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

        player.transform.position += new Vector3(0, player.FloatUpSpeed * Time.deltaTime, 0);
        if (player.IsCeiling)
        {
            fsm.ChangeState<PlayerAirState>();
        }
    }
    public static PlayerJumpState Create(string animName)
    {
        return new PlayerJumpState(animName);
    }
}