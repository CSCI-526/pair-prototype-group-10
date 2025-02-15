using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondaryAttackState : PlayerState
{
    private int secondaryAttackCombo;
    public PlayerSecondaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        secondaryAttackCombo = player.comboCounter;

        player.followUpHitBoxSize.x = 12.0f;
        player.followUpHitBoxSize.y = 8.0f;
        player.followUpHitBoxCenterOffset.x = 4.0f;
        player.followUpHitBoxCenterOffset.y = 1.5f;
    }

    public override void Exit()
    {
        base.Exit();
        player.showFollowUpHitBox = false;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if(followUpEnabled)
        {
            player.showFollowUpHitBox = true;
        }
    }
}
