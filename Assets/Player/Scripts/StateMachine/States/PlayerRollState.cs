
using System.Collections;
using UnityEngine;


public class PlayerRollState : PlayerState
{

    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {

    }

    public override void Enter()
    {

        Roll();
        player.effectController.DashEffectActivate();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.effectController.NormalProfile();
        player.StartCoroutine(DashCooldown());
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        player.controller.Move(player.desiredMoveDirection.normalized * 30f * Time.deltaTime);
        if (isRollAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(player.MeleeState);
        }
    }

    private void Roll()
    {
        player.Anim.SetTrigger("isRolling");       
    }

    IEnumerator DashCooldown()
    {
        player.CanDash = false;
        yield return new WaitForSeconds(0.5f);
        player.CanDash = true;
    }

}
