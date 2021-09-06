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


    public override void Start()
    {   
        base.Start();
        moveState = new E1_MoveState(this, stateMachine, this);
        idleState = new E1_IdleState(this, stateMachine, this);
        knockBackState = new E1_KnockBackState(this, stateMachine, this);
        deathState = new E1_DeathState(this, stateMachine, this);
        attackState = new E1_AttackState(this, stateMachine, this);

        stateMachine.Initialize(idleState);
    }

    public void PlayHitParticle()
    {
        hitEffect.Play();
    }

    

}
