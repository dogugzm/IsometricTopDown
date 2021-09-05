using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : PlayerState
{


    public PlayerMeleeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        MeleeAttack();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.Speed = 0;
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        player.controller.Move(player.desiredMoveDirection * 2f * Time.deltaTime);
        if (isMeleeAnimationFinished)
        {
            if (player.Speed > 0.1f)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (player.Speed < 0.1f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    private void MeleeAttack()
    {   
       player.ChangeRotationToCursor();
       CinemachineShake.instance.ShakeCamera(1f, 0.5f);
       player.Anim.SetTrigger("isAttacking");                 
    }
}
