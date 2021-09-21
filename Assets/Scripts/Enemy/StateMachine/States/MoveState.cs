using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    
    public MoveState(Entity entity, FiniteStateMachine stateMachine,string name) : base(entity, stateMachine,name)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
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
