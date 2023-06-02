using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerCombat3State : PlayerState
{
    Transform target;

    EquipmentController.Equipment swordState =  EquipmentController.Equipment.Sword;

    public PlayerCombat3State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {


    }

    public override void Enter()
    {
        base.Enter();
        target = null;
        player.fieldOfViewScript.Combat3ValuesActivate();  
        player.Anim.ResetTrigger("isAttacking");                 
        player.equipmentController.ChangeState(swordState);
        player.Anim.SetTrigger("isAttacking3");
        player.Sword.SetActive(true);
        player.SwordParticle.Play();

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
        player.fieldOfViewScript.GoDefaultValues();
        DOTween.KillAll();

    }

    public override void LogicalUpdate()
    {

        base.LogicalUpdate();
        if (target != null)
        {
            player.transform.DOLookAt(target.position, 0.2f);
            player.transform.DOMove(player.TargetOffset(target), 1f);
        }

        if (isCombat3AnimationFinished)
        {
            
            if (player.Speed >= 0.1f)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (player.Speed < 0.1f)
            {
                stateMachine.ChangeState(player.IdleState);
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
