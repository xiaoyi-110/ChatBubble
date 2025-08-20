public class PlayerAirState : PlayerState
{
    public PlayerAirState(string animName) : base(animName)
    {
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        if(hasChanged)return;
        player.Attack();

        if(!player.IsTryAir)
        {
            fsm.ChangeState<PlayerFallState>();
        }
    }

    override public void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        fsm.Data = typeof(PlayerAirState);
        
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        base.OnLeave(fsm);
       
    }

    public static PlayerAirState Create(string animName)
    {
        return new PlayerAirState(animName);
    }

}

