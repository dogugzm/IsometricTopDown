using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DeathState : DeathState
{
    private Enemy1 enemy;
    public E1_DeathState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy,string name) : base(entity, stateMachine,name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
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
