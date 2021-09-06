using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{
    private Enemy1 enemy;
    public E1_IdleState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy) : base(entity, stateMachine)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        
        base.Enter();
        enemy.anim.SetBool("isIdle", true);

    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("isIdle", false);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.GetDistanceBetweenPlayer()<enemy.distance)
        {
            stateMachine.ChangeState(enemy.moveState);
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
