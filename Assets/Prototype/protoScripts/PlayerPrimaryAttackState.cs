using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked; 
    private float comboWindow = 1f;

    //For GUI TESTING
    public int ComboCounterGUI => comboCounter;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("comboCounter", comboCounter);

        float attackDir = player.facingDir;
        if(xInput != 0)
        {
            attackDir = xInput;
        }
        player.setVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine(player.Busyfor(0.2f));
        comboCounter++;
        lastTimeAttacked = Time.time;

    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.ZeroVelocity();
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
