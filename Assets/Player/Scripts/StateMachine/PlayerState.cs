using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected string stateName;
    protected float startTime;
    protected bool isRollAnimationFinished;
    protected bool isMeleeAnimationFinished;


    public PlayerState(Player player,PlayerStateMachine stateMachine,PlayerData playerData,string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.stateName = stateName;       
    }

    public virtual void Enter()
    {
        player.Anim.SetFloat("Blend", 0,0.01f,Time.deltaTime);   
        startTime = Time.time;
        isRollAnimationFinished = false;
        isMeleeAnimationFinished = false;

        Debug.Log(stateName);
    }
    public virtual void Exit()
    {

    }

    public virtual void LogicalUpdate()
    {

    }

    public virtual void RollAnimationFinishTrigger()
    {
        isRollAnimationFinished = true;
    }
    public virtual void MeleeAnimationFinishTrigger()
    {
        isMeleeAnimationFinished = true;
    }



}
