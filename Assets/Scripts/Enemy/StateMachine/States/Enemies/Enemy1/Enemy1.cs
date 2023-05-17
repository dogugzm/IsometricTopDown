using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public ParticleSystem hitEffect;

    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_KnockBackState knockBackState { get; private set; }
    public E1_DeathState deathState { get; private set; }

    public E1_AttackState attackState { get; private set; }
    public E1_DriftState driftState { get; private set; }

    public override void Start()
    {   
        base.Start(); 
        moveState = new E1_MoveState(this, stateMachine, this,"Move");
        idleState = new E1_IdleState(this, stateMachine, this,"Idle");
        knockBackState = new E1_KnockBackState(this, stateMachine, this,"Hit");
        deathState = new E1_DeathState(this, stateMachine, this,"Death");
        attackState = new E1_AttackState(this, stateMachine, this,"Attack");
        driftState = new E1_DriftState(this, stateMachine, this, "Drift");
        stateMachine.Initialize(idleState);
    }

    public override void OnHit()
    {
        base.OnHit();
        
        stateMachine.ChangeState(knockBackState);
    }

    public override void OnHitGreate()
    {
        base.OnHit();

        stateMachine.ChangeState(driftState);
    }

    public void PlayHitParticle()
    {
        hitEffect.Play();
    }

    
}
