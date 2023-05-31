using System.Collections;
using UnityEngine;

public class EBow_AttackState : AttackState
{
    EnemyBow enemy;
    Vector3 lastSeenPlayerLoc;

    public EBow_AttackState(Entity entity, FiniteStateMachine stateMachine, EnemyBow enemy, string name) : base(entity, stateMachine, name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Anim.SetTrigger("isAttacking");
        enemy.isAttackAnimFinished = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.LookAtPlayer();

        //if (enemy.isParried)
        //{
        //    stateMachine.ChangeState(enemy.parriedState);
        //    return;
        //}
        //if (!animationPlayed)
        //{
        //    enemy.agent.SetDestination(enemy.Target.position);
        //}
        //
        if (enemy.isAttackAnimFinished)
        {
            enemy.StartCoroutine(InstantiateBowProjectile());            
        }
        
    }

    IEnumerator InstantiateBowProjectile()
    {
        yield return new WaitForSeconds(0f);    
        Enemy.Instantiate(enemy.Projectile, enemy.ProjectilePosition.position, enemy.ProjectilePosition.rotation);
        stateMachine.ChangeState(enemy.idleState);
    }


}
