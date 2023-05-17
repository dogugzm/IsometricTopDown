using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

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
        enemy.agent.ResetPath();
        enemy.StartCoroutine(enemy.ChangeStateInSeconds(2, enemy.attackState));
    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.ResetPath();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.LookAtPlayer();


    }


}
