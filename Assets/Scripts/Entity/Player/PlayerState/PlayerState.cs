public class PlayerState : FSMState<Player>
{
    protected string animName;
    protected Player player;
    protected bool hasChanged;

    public PlayerState(string animName)
    {
        this.animName = animName;
    }

    public override void OnEnter(FSM<Player> fsm)
    {
        base.OnEnter(fsm);
        player = fsm.Owner;
        player.Animator.SetBool(animName, true);
        hasChanged = false;
    }

    public override void OnLeave(FSM<Player> fsm)
    {
        base.OnLeave(fsm);
        player.Animator.SetBool(animName, false);
        hasChanged = true;
    }

    public override void OnUpdate(FSM<Player> fsm)
    {
        base.OnUpdate(fsm);
        if (hasChanged) return;

        if (player.IsHurtInvincible)
        {
            hasChanged = true;
            fsm.ChangeState<PlayerAttackedState>();
        }
    }
}
