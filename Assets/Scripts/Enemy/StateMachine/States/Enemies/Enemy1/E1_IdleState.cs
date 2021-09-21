using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{
    private Enemy1 enemy;

    public E1_IdleState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy ,string name) : base(entity, stateMachine, name)
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
        if (enemy.GetDistanceBetweenPlayer()<enemy.distance && enemy.GetDistanceBetweenPlayer()>3f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (enemy.goToHurtState)
        {
            stateMachine.ChangeState(enemy.knockBackState);
        }
        else if (enemy.IsEnemyDead() && stateMachine.currentState != enemy.deathState)
        {
            stateMachine.ChangeState(enemy.deathState);
        }

    }
}
