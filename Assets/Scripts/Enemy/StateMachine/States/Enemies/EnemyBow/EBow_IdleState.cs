using System.Collections;
using UnityEngine;

public class EBow_IdleState : IdleState
{
    private EnemyBow enemy;
    IEnumerator coroutine;

    public EBow_IdleState(Entity entity, FiniteStateMachine stateMachine, EnemyBow enemy ,string name) : base(entity, stateMachine, name)
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
