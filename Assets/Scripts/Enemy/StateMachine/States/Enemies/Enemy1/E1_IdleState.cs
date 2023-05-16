using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Child");

        enemy.agent.ResetPath();
        enemy.agent.updateRotation = false;

        //enemy.anim.SetBool("isIdle", true);
        enemy.StartCoroutine(enemy.ChangeStateInSeconds(2, enemy.attackState));  
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.anim.SetBool("isIdle", false);
        enemy.agent.updateRotation = true;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.transform.DOLookAt(new Vector3(enemy.Target.position.x, 0.8f, enemy.Target.position.z), 0.2f);



    }


}
