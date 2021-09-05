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
        //move anim oynat
        
    }

    public override void Exit()
    {
        base.Exit();
        //move anim kapa
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.agent.SetDestination(enemy.Target.position);

        if (entity.GetDistanceBetweenPlayer()<1f)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        if (enemy.goToHurtState)
        {
            stateMachine.ChangeState(enemy.knockBackState);

        }
        if (enemy.IsEnemyDead())
        {
            stateMachine.ChangeState(enemy.deathState);

        }

    }
}
