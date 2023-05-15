using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerHeavyAttackState : PlayerState
{
    Transform target;

    EquipmentController.Equipment swordState = EquipmentController.Equipment.Sword;

    public PlayerHeavyAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {

    }

    public override void Enter()
    {

        base.Enter();
        player.fieldOfViewScript.Combat3ValuesActivate();
        player.equipmentController.ChangeState(swordState);
        player.Anim.SetTrigger("isHeavyAttacking");
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
        player.controller.Move(Vector3.zero);
        player.fieldOfViewScript.GoDefaultValues();
        DOTween.KillAll(); //maybe just on this script.

    }

    public override void LogicalUpdate()
    {

        base.LogicalUpdate();

        if (target != null)
        {
            player.transform.DOLookAt(target.position, 0.2f);
            player.transform.DOMove(player.TargetOffset(target), 1f);
        }

        if (isHeavyAttackAnimationFinished)
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
