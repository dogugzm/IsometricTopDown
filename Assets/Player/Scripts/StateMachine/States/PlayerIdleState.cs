using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        player.Anim.SetFloat("Blend", player.Speed, player.StartAnimTime, Time.deltaTime);
        if (player.Speed > 0.1f)
        {
           stateMachine.ChangeState(player.MoveState);
        }
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.MeleeState);
        }
        if (Input.GetMouseButtonDown(1))
        {
            stateMachine.ChangeState(player.ShootState);
        }

    }
}
