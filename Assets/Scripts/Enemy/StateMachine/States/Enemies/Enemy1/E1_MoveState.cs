using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;

    public E1_MoveState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy,string name) : base(entity, stateMachine,name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.updateRotation = false;  

    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.updateRotation = true;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.LookAtPlayer();
        if (enemy.GetDistanceBetweenPlayer()<5)
        {
            enemy.agent.destination += enemy.GetDirectionToPlayer() * -5f;           
            //enemy.agent.Move(enemy.GetDirectionToPlayer() * -5 * Time.deltaTime);
        }
        else
        {
            stateMachine.ChangeState(enemy.idleState);
        }

    }
}
