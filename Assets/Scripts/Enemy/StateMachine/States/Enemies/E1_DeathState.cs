using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DeathState : State
{
    private Enemy1 enemy;
    public E1_DeathState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //particle, anim vb oynat
        entity.DestroyMe();
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
