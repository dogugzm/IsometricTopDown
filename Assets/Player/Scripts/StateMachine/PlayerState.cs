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
    protected bool isCombat2AnimationFinished;
    protected bool isCombat3AnimationFinished;
    protected bool isDashAttackAnimationFinished;



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
        player.currentStateText = stateName;
        startTime = Time.time;
        isRollAnimationFinished = false;
        isMeleeAnimationFinished = false;
        isCombat2AnimationFinished = false;
        isCombat3AnimationFinished = false;
        isDashAttackAnimationFinished = false;


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
    public virtual void Combat2AnimationFinishTrigger()
    {
        isCombat2AnimationFinished = true;
    }
    public virtual void Combat3AnimationFinishTrigger()
    {
        isCombat3AnimationFinished = true;
    }
    public virtual void DashAttackAnimationFinishTrigger()
    {
        isDashAttackAnimationFinished = true;
    }



}
