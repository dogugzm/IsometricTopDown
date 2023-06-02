using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBow_DeathState : DeathState
{
    private EnemyBow enemy;
    public EBow_DeathState(Entity entity, FiniteStateMachine stateMachine, EnemyBow enemy,string name) : base(entity, stateMachine,name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.ResetPath();
        enemy.rb.isKinematic = true;
        enemy.Anim.SetBool("isDead",true);    
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
    }
}
