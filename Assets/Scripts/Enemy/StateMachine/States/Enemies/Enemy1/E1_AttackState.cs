using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class E1_AttackState : AttackState
{
    Enemy1 enemy;
    bool animationPlayed;
    Vector3 lastSeenPlayerLoc;

    public E1_AttackState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy,string name) : base(entity, stateMachine,name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();       
        animationPlayed = false;
        enemy.isAttackAnimFinished = false;

           
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.anim.SetBool("isAttack", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!animationPlayed)
        {
            enemy.agent.SetDestination(enemy.Target.position);
        }
        if (enemy.IsEnemyHasReachedDestiantion() && !animationPlayed)
        {
            animationPlayed = true;
            enemy.Anim.SetTrigger("isAttacking");
        }
        if (enemy.isAttackAnimFinished)
        {
             stateMachine.ChangeState(enemy.moveState);

        }

        //if (enemy.goToHurtState)
        //{
        //    stateMachine.ChangeState(enemy.knockBackState);
        //}
        //else if (enemy.isAttackAnimFinished || enemy.GetDistanceBetweenPlayer() > 3f)
        //{
        //    stateMachine.ChangeState(enemy.idleState);
        //}
    }


}
