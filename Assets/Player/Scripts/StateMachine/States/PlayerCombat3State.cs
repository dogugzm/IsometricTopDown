using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat3State : PlayerState
{

    EquipmentController.Equipment swordState =  EquipmentController.Equipment.Sword;

    public PlayerCombat3State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.fieldOfViewScript.Combat3ValuesActivate();  
        player.Anim.ResetTrigger("isAttacking");                 
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
        player.fieldOfViewScript.GoDefaultValues();

    }

    public override void LogicalUpdate()
    {

        base.LogicalUpdate();
        //player.controller.Move(2f * Time.deltaTime * player.desiredMoveDirection);

        // if (Input.GetMouseButtonDown(0))
        // {
        //     stateMachine.ChangeState(player.Combat2State);
            
        // }
       
        if (isCombat3AnimationFinished)
        {
            Debug.Log("finished");
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
       CinemachineShake.instance.ShakeCamera(1.2f, 0.5f);
       player.Anim.SetTrigger("isAttacking3");                 
    }
}
