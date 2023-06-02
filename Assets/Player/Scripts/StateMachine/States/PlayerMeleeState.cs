using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMeleeState : PlayerState
{

    EquipmentController.Equipment swordState =  EquipmentController.Equipment.Sword;
    bool isCombatContinue;

    public PlayerMeleeState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {

    }

    Transform target;

    public override void Enter()
    {

        base.Enter();
        target = null;
        isCombatContinue = false;
        player.Anim.ResetTrigger("isAttacking");                 
        player.equipmentController.ChangeState(swordState);
        player.Anim.SetTrigger("isAttacking");     
        player.Sword.SetActive(true);
        player.SwordParticle.Play(); //TODO:4_SwordTrail

        if (player.currentEnemy != null)
        {
            if (player.currentEnemy.IsLastHit())
            {
                player.StartCoroutine(FinalCutDeath());
            }     
            target = player.currentEnemy.transform;
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        player.Speed = 0;
        player.SwordParticle.Stop();
        player.Sword.SetActive(false);
        player.controller.Move(Vector3.zero);
        DOTween.KillAll(); //maybe just on this script.
    }

    public override void LogicalUpdate()
    {

        base.LogicalUpdate();

        if (target!= null)
        {            
            player.transform.DOLookAt(target.position, 0.2f);
            player.transform.DOMove(player.TargetOffset(target), 1f);


        }

        if (Input.GetMouseButtonDown(0))
        {
            isCombatContinue = true;
                 
        }

        if (isMeleeAnimationFinished)
        {
            if (isCombatContinue)
            {
                stateMachine.ChangeState(player.Combat2State);
            }
            else 
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

    IEnumerator FinalCutDeath() 
    {
        Time.timeScale = 0.5f;
        player.CinematicCamera.SetActive(true);
        player.CinematicCameraFocusObject.position = player.currentEnemy.transform.position;
        yield return new WaitForSecondsRealtime(2f);
        player.CinematicCamera.SetActive(false);
        Time.timeScale = 1f;

    }

}
