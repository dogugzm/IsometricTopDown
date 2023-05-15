using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat2State : PlayerState
{
    Transform target;

    EquipmentController.Equipment swordState =  EquipmentController.Equipment.Sword;
    bool isCombotContinue;

    public PlayerCombat2State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isCombotContinue = false;
        //player.Anim.ResetTrigger("isAttacking2");                 
        player.equipmentController.ChangeState(swordState);
        player.Anim.SetTrigger("isAttacking2");                 
        player.Sword.SetActive(true);
        player.SwordParticle.Play();
        if (player.currentEnemy != null)
        {
            if (player.currentEnemy.IsLastHit())
            {
                player.StartCoroutine(FinalCutDeath());
            }
        }
        target = player.currentEnemy.transform;



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
        if (target != null)
        {
            player.transform.DOLookAt(target.position, 0.2f);
            player.transform.DOMove(player.TargetOffset(target), 1f);
        }

       
        if (Input.GetMouseButtonDown(0))
        {
            isCombotContinue = true;                 
        }
        
        if (isCombat2AnimationFinished)
        {
            
            if (isCombotContinue)
            {
                stateMachine.ChangeState(player.Combat3State);
                return;
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
