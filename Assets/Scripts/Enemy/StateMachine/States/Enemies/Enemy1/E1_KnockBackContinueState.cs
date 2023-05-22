using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_KnockBackContinueState : KnockBackState
{
    private Enemy1 enemy;
    

    public E1_KnockBackContinueState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy,string name) : base(entity, stateMachine,name)
    {
        this.enemy = enemy;
    }

    public override void Enter()    
    {
        base.Enter();      
        enemy.Anim.SetBool("isHit2", true);
        enemy.PlayHitParticle();   
        enemy.DecreaseHealth(enemy.damageTaken);
        enemy.isHit2AnimFinished = false;
        //enemy.StartCoroutine(Effect());
    }

    public override void Exit()
    {
        base.Exit();     
        enemy.Anim.SetBool("isHit2", false);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();   

        enemy.agent.Move(enemy.GetDirectionToPlayer() * -2f * Time.deltaTime);

        if (enemy.isHit2AnimFinished)
        {
            stateMachine.ChangeState(enemy.idleState);

        }

    }
    //IEnumerator Effect()
    //{
    //    enemy.agent.speed = 10;
    //    enemy.agent.angularSpeed = 0;
    //    enemy.agent.acceleration = 8;
    //    enemy.agent.velocity = enemy.Target.transform.forward * enemy.knockBackDistance;
    //    //enemy.meshRend.material.SetColor("_EmissionColor", Color.white);
    //    //enemy.meshRend.material.EnableKeyword("_EMISSION");
    //    //yield return new WaitForSeconds(0.1f);
    //    //enemy.meshRend.material.SetColor("_EmissionColor", Color.black);

    //    yield return new WaitForSeconds(1f);

    //    enemy.agent.speed = 3.5f;
    //    enemy.agent.angularSpeed = 120;
    //    enemy.agent.acceleration = 8;
       
    //    stateMachine.ChangeState(enemy.idleState);

        
    //}
}
