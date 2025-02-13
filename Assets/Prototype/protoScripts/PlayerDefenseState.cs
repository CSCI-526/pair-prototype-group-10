using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenseState : PlayerState
{
    public PlayerDefenseState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.setVelocity(0f, 0f);
    }

    public override void Exit()
    {
        base.Exit();
        //reset color
        player.spriteRenderer.color = Color.white;
    }

    public override void Update()
    {
        base.Update();

        //prototype show parry invincible
        player.spriteRenderer.color = Color.blue;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            stateMachine.ChangeState(player.idleState);  // Return to Grounded when space is released
        }
    }
}
