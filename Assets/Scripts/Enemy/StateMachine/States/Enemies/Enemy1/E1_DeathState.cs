using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DeathState : State
{
    private Enemy1 enemy;
    public E1_DeathState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy) : base(entity, stateMachine)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool("isDeath", true);
        //particle, anim vb oynat
        entity.DestroyMe(1f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
