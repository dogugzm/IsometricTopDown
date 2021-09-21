using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    
    public IdleState(Entity entity, FiniteStateMachine stateMachine,string name) : base(entity, stateMachine,name)
    {
        
    }

    public override void Enter() 
    {
        base.Enter();
        
        entity.agent.isStopped = true;
        entity.agent.ResetPath();
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
