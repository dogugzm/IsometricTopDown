using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        player.Anim.SetFloat("Blend", 0);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();

        player.Anim.SetFloat("Blend", player.Speed, player.StopAnimTime, Time.deltaTime);
        MoveInputs();
        
        if (player.Speed < 0.1f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.RollState);
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

    void MoveInputs()
    {
        player.InputX = Input.GetAxis("Horizontal");
        player.InputZ = Input.GetAxis("Vertical");

        var cam = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        player.desiredMoveDirection = forward * player.InputZ + right * player.InputX;
        player.controller.Move(player.desiredMoveDirection * Time.deltaTime * player.Velocity);

    }
    
}
