using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerState
{
    public PlayerShootState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
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
        if (Input.GetMouseButton(1))
        {
            player.ChangeRotationToCursor();

        }
        if (Input.GetMouseButtonUp(1))
        {
            Player.mouseClickedDir = player.transform.forward;
            Player.closestPosition.y = Player.closestPosition.y + 1;
            Player.Instantiate(player.MagicBall, Player.closestPosition, Quaternion.identity);
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
