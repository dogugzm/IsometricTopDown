using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_KnockBackState : KnockBackState
{
    private Enemy1 enemy;
    float startTime;

    public E1_KnockBackState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy) : base(entity, stateMachine)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        startTime = 0;
        enemy.DecreaseHealth(enemy.damageTaken);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        base.LogicUpdate();
        startTime++;

        enemy.agent.velocity = enemy.Target.transform.forward * 3;
        enemy.agent.speed = 50;
        enemy.agent.angularSpeed = 0;
        enemy.agent.acceleration = 30;

        if (startTime > 1f)
        {

            enemy.agent.speed = 3.5f;
            enemy.agent.angularSpeed = 120;
            enemy.agent.acceleration = 8;
            startTime = 0;
            stateMachine.ChangeState(enemy.idleState);

            if (enemy.IsEnemyDead())
            {
                stateMachine.ChangeState(enemy.deathState);

            }
        }
        
    }
}
