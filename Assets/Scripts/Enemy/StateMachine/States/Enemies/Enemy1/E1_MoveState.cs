using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;

    public E1_MoveState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy) : base(entity, stateMachine)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool("isRun", true);


    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("isRun", false);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.agent.SetDestination(enemy.Target.position);

        if (entity.GetDistanceBetweenPlayer()<3f)
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        if (enemy.goToHurtState)
        {
            stateMachine.ChangeState(enemy.knockBackState);

        }
        if (enemy.IsEnemyDead())
        {
            stateMachine.ChangeState(enemy.deathState);

        }
        if (entity.GetDistanceBetweenPlayer() > enemy.distance)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

    }
}
