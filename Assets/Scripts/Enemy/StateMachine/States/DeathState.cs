using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{   
    public DeathState(Entity entity, FiniteStateMachine stateMachine,string name) : base(entity, stateMachine,name)
    {
        
    }

    public override void Enter() 
    {
        base.Enter();
        entity.Target.GetComponent<Player>().ClearCurrentEnemyIfSame(entity);
        entity.GetComponent<Collider>().enabled = false;
        entity.isDeath = true;
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
