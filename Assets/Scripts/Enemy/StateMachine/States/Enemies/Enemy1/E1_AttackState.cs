using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_AttackState : AttackState
{
    Enemy1 enemy;
    public E1_AttackState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy,string name) : base(entity, stateMachine,name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isAttackAnimFinished = false;
        enemy.StartCoroutine(enemy.WaitTime(1f));
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
        if (enemy.goToHurtState)
        {
            stateMachine.ChangeState(enemy.knockBackState);
        }
        else if (enemy.isAttackAnimFinished || enemy.GetDistanceBetweenPlayer() > 3f)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
