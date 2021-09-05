using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackState : State
{
    
    public KnockBackState(Entity entity, FiniteStateMachine stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.goToHurtState = false;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        
        
        
    }

   
}
