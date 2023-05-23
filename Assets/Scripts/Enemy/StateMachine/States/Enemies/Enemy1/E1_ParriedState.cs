using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ParriedState : ParriedState
{
    private Enemy1 enemy;
    bool justOne;

    public E1_ParriedState(Entity entity, FiniteStateMachine stateMachine,Enemy1 enemy,string name) : base(entity, stateMachine,name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();      
        enemy.Anim.SetBool("isParried", true);
        justOne = true;
        //enemy.PlayHitParticle();   
        //enemy.DecreaseHealth(enemy.damageTaken);
        //enemy.isHitAnimFinished = false;
    }
        
    public override void Exit()
    {
        base.Exit();
        //enemy.goToHurtState = false;
        //enemy.isParried = false;
        enemy.isParryAnimFinished = false;
        enemy.StopCoroutine(Parried());
        enemy.SetParriableFalse();
        enemy.Anim.SetBool("isParried", false);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        enemy.agent.Move(enemy.GetDirectionToPlayer() * -5f * Time.deltaTime);

        //if (enemy.IsEnemyDead() && stateMachine.currentState !=enemy.deathState)
        //{            
        //    stateMachine.ChangeState(enemy.deathState);
        //}

        if (enemy.isParryAnimFinished && justOne)
        {
            enemy.StartCoroutine(Parried());
        }

    }

    IEnumerator Parried()
    {
        justOne = false;
        yield return new WaitForSeconds(2f);
        stateMachine.ChangeState(enemy.idleState);
    }
 
}
