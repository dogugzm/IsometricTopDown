using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    public PlayerParryState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {


    }

    Transform target;

    public override void Enter()
    {
        base.Enter();
        player.Anim.SetTrigger("isParried");
        player.Sword.SetActive(true);
        player.SwordParticle.Play();
        //player.ParryParticle.Play();
        target = player.ParriableEnemies[0].transform;
    }

    public override void Exit()
    {
        base.Exit();
        player.Anim.ResetTrigger("isParried");
        player.SwordParticle.Stop();
        player.ParryParticle.Stop();
        player.Sword.SetActive(false);
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        if (target != null)
        {
            player.transform.DOLookAt(target.position, 0.2f);
            player.transform.DOMove(player.TargetOffset(target), 1f);
        }

        if (isParryAnimationFinished)
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

}


