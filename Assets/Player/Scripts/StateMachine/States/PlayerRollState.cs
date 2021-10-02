using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerRollState : PlayerState
{
    
    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        Roll();
        player.effectController.DashEffectActivate();
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
        player.effectController.NormalProfile();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        player.controller.Move(player.desiredMoveDirection * 15f * Time.deltaTime);
        if (isRollAnimationFinished)
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

    private void Roll()
    {
        player.Anim.SetTrigger("isRolling");
    }

    

}
