
using System.Collections;
using UnityEngine;


public class PlayerRollState : PlayerState
{

    bool dashCombatActivated;

    public PlayerRollState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string stateName) : base(player, stateMachine, playerData, stateName)
    {

    }

    public override void Enter()
    {

        Roll();
        player.effectController.DashEffectActivate();
        base.Enter();
        dashCombatActivated = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine(DashCooldown());
        if (dashCombatActivated)
        {
            return;
        }
        player.effectController.NormalProfile();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        player.controller.Move(player.desiredMoveDirection.normalized * 10f * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            dashCombatActivated = true;
        }
        if (isRollAnimationFinished)
        {
            if (dashCombatActivated)
            {
                stateMachine.ChangeState(player.DashCombatState);


            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
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
