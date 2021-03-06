using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_KnockBackState : KnockBackState
{
    private Enemy1 enemy;
    

    public E1_KnockBackState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy,string name) : base(entity, stateMachine,name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        enemy.anim.SetBool("isHit", true);
        enemy.PlayHitParticle();
     
        enemy.DecreaseHealth(enemy.damageTaken);
        enemy.StartCoroutine(Effect());

    }

    public override void Exit()
    {
        base.Exit();
        enemy.goToHurtState = false;
        enemy.anim.SetBool("isHit", false);


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy.IsEnemyDead() && stateMachine.currentState !=enemy.deathState)
        {
            
            stateMachine.ChangeState(enemy.deathState);

        }

    }
    IEnumerator Effect()
    {
        enemy.agent.speed = 10;
        enemy.agent.angularSpeed = 0;
        enemy.agent.acceleration = 8;
        enemy.agent.velocity = enemy.Target.transform.forward * enemy.knockBackDistance;
        enemy.meshRend.material.SetColor("_EmissionColor", Color.white);
        enemy.meshRend.material.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.1f);
        enemy.meshRend.material.SetColor("_EmissionColor", Color.black);



        yield return new WaitForSeconds(1f);


        enemy.agent.speed = 3.5f;
        enemy.agent.angularSpeed = 120;
        enemy.agent.acceleration = 8;
       

        

        stateMachine.ChangeState(enemy.idleState);

        
    }
}
