using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startingTime;

    public State(Entity entity,FiniteStateMachine stateMachine)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        startingTime = Time.time;
    }
    public virtual void Exit()
    {

    }
    public virtual void LogicUpdate()
    {

    }



}
