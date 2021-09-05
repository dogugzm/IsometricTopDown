using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    
    public MoveState(Entity entity, FiniteStateMachine stateMachine) : base(entity, stateMachine)
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
