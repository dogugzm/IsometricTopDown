using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2State : PlayerState
{

    EquipmentController.Equipment swordState =  EquipmentController.Equipment.Sword;

    public PlayerCombat2State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //player.Anim.ResetTrigger("isAttacking");                 
        player.equipmentController.ChangeState(swordState);
        MeleeAttack();
        player.Sword.SetActive(true);
        player.SwordParticle.Play();

    }

    public override void Exit()
    {
        base.Exit();
        player.Speed = 0;
        player.SwordParticle.Stop();
        player.Sword.SetActive(false);

    }

    public override void LogicalUpdate()
    {
        
        base.LogicalUpdate();
        player.controller.Move(player.desiredMoveDirection * 2f * Time.deltaTime);


         if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.Combat3State);
            return;
        }

        

        if (isCombat2AnimationFinished)
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
       CinemachineShake.instance.ShakeCamera(1.1f, 0.5f);
       player.Anim.SetTrigger("isAttacking");                 
    }
}
