using UnityEngine;

public class EnemyBow : Entity
{
    public ParticleSystem hitEffect;
    public GameObject Projectile;
    public Transform ProjectilePosition;


    public EBow_IdleState idleState { get; private set; }
    public EBow_MoveState moveState { get; private set; }
    public EBow_AttackState attackState { get; private set; }
    public EBow_KnockBackState knockBackState { get; private set; }
    public EBow_KnockBackContinueState knockBackContinueState { get; private set; }
    public EBow_DriftState driftState { get; private set; }
    public EBow_DeathState deathState { get; private set; }
    //public E1_ParriedState parriedState { get; private set; }

    public override void Start()
    {
        base.Start();
        moveState = new EBow_MoveState(this, stateMachine, this, "Move");
        idleState = new EBow_IdleState(this, stateMachine, this, "Idle");
        knockBackState = new EBow_KnockBackState(this, stateMachine, this, "Hit");
        deathState = new EBow_DeathState(this, stateMachine, this, "Death");
        attackState = new EBow_AttackState(this, stateMachine, this, "Attack");
        driftState = new EBow_DriftState(this, stateMachine, this, "Drift");
        knockBackContinueState = new EBow_KnockBackContinueState(this, stateMachine, this, "Hit2");
        //parriedState = new E1_ParriedState(this, stateMachine, this, "Parried");
        stateMachine.Initialize(idleState);
    }

    public override void OnHit()
    {
        base.OnHit();
        if (stateMachine.currentState == knockBackState)
        {
            stateMachine.ChangeState(knockBackContinueState);
        }
        else
        {
            stateMachine.ChangeState(knockBackState);
        }
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

    public override void ParriedState()
    {
        base.ParriedState();
        //stateMachine.ChangeState(parriedState);
    }

    public override bool IsInDamagableState()
    {
        if (stateMachine.currentState == deathState)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
