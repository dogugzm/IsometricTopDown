using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeState : PlayerState
{

    EquipmentController.Equipment swordState =  EquipmentController.Equipment.Sword;

    public PlayerMeleeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.Anim.ResetTrigger("isAttacking");                 
        player.equipmentController.ChangeState(swordState);
        MeleeAttack();
        
        player.Sword.SetActive(true);
        player.SwordParticle.Play(); //TODO:4_SwordTrail

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
        //Debug.Log(player.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        base.LogicalUpdate();
        player.controller.Move(1.2f * Time.deltaTime * player.desiredMoveDirection); //TODO:3_PlayerMove

        if (Input.GetMouseButtonDown(0))
        {
            
            stateMachine.ChangeState(player.Combat2State);
            return;
            
        }

        if (isMeleeAnimationFinished)
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
       //CinemachineShake.instance.ShakeCamera(1f, 0.5f); //TODO:6_ShakeCamera
       player.Anim.SetTrigger("isAttacking");                 
    }


    IEnumerator AfterAnim()
    {
        yield return new WaitForSeconds(1);
    }
}
