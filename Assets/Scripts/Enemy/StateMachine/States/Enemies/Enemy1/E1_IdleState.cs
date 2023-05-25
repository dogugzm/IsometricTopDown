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
    IEnumerator coroutine;
    public E1_IdleState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy ,string name) : base(entity, stateMachine, name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {        
        base.Enter();
        enemy.agent.ResetPath();
        coroutine = enemy.ChangeStateInSeconds(Random.Range(2,5), enemy.attackState);
        enemy.StartCoroutine(coroutine);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.ResetPath();
        enemy.StopCoroutine(coroutine);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.LookAtPlayer();


    }


}
