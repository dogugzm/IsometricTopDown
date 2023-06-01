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
        if (enemy.GetDistanceBetweenPlayer() > 15)
        {
            coroutine = enemy.ChangeStateInSeconds(Random.Range(2, 4), enemy.attackState);
        }
        else
        {
            coroutine = enemy.ChangeStateInSeconds(Random.Range(2,5), enemy.moveState);
        }
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
