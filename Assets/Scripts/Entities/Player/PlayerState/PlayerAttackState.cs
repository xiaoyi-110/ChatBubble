
using Mono.CompilerServices.SymbolWriter;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboCounter;
    private float comboTimeWindow;
    private float lastAttackTime;


    public PlayerAttackState(Player _player, string _animBoolName) : base(_player, _animBoolName)
    {
        moveSpeedScale = 0;
        comboCounter = 0;
    }

    public override void Enter()
    {

        base.Enter();
        comboTimeWindow = player.comboTimeWindow;
        if (Time.time - lastAttackTime >= comboTimeWindow)
        {
            comboCounter = 0;
        }
        player.animator.SetInteger("comboCounter", comboCounter);

    }

    public override void Exit()
    {
        base.Exit();
        comboCounter = (comboCounter + 1) % 3;
        lastAttackTime = Time.time;

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            player.stateMachine.ChangeState(typeof(PlayerIdleState));
        }
    }

}