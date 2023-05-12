using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashCombatState : PlayerState
{
    public PlayerDashCombatState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetBool("isDashAttacking",true);
        player.StartCoroutine(player.ActivateSwordWithDelay(0.5f, true));
        player.fieldOfViewScript.Combat3ValuesActivate();
    }



    public override void Exit()
    {
        base.Exit();
        player.Anim.SetBool("isDashAttacking", false);
        player.StartCoroutine(player.ActivateSwordWithDelay(0f, false));
        player.effectController.NormalProfile();
        player.fieldOfViewScript.GoDefaultValues();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        player.controller.Move(player.desiredMoveDirection.normalized * 5f * Time.deltaTime);

        if (isDashAttackAnimationFinished) 
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
