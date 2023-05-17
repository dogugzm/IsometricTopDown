using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class E1_DriftState : IdleState
{
    private Enemy1 enemy;

    public E1_DriftState(Entity entity, FiniteStateMachine stateMachine, Enemy1 enemy, string name) : base(entity, stateMachine, name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.ResetPath();
        enemy.Anim.SetBool("isHit", true);

    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.ResetPath();
        enemy.Anim.SetBool("isHit", false);


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.agent.Move(enemy.GetDirectionToPlayer() * -10f * Time.deltaTime);




    }


}
