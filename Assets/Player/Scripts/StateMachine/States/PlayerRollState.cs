using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    
    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        Roll();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        player.controller.Move(player.desiredMoveDirection * 7f * Time.deltaTime);
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

        //animasyon bittiðinde ýdle a dön
    }

    private void Roll()
    {


        
        player.Anim.SetTrigger("isRolling");
        player.StartCoroutine(VolumeTest.instance.RollEffect());

    }

    

}
