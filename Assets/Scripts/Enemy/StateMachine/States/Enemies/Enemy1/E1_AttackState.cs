using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_AttackState : AttackState
{
    Enemy1 enemy;
    public E1_AttackState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy) : base(entity, stateMachine)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool("isAttack", true);

        //anim baþlat ve anim içinde trigger oto oynayacak, attack methodu entity içinde olacak.
    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("isAttack", false);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy.GetDistanceBetweenPlayer()<3f)
        {
            return;
        }
        if (enemy.isAttackAnimFinished)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        if (enemy.goToHurtState)
        {
            stateMachine.ChangeState(enemy.knockBackState);

        }


    }
}
