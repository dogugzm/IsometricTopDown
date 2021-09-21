using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetTrigger("isHitted");
        player.health -= player.damageTaken;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
        player.StartCoroutine(KnockBack());
       

    }

    IEnumerator KnockBack()
    {
        player.controller.Move(-player.transform.forward * 2f * Time.deltaTime * 2f);
        yield return new WaitForSeconds(1f);
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


