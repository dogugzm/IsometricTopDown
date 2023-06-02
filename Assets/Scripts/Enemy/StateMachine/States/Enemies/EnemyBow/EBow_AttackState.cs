using System.Collections;
using UnityEngine;

public class EBow_AttackState : AttackState
{
    EnemyBow enemy;
    bool attacked;

    public EBow_AttackState(Entity entity, FiniteStateMachine stateMachine, EnemyBow enemy, string name) : base(entity, stateMachine, name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        attacked = false;
        enemy.agent.ResetPath();
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


        if (enemy.isAttackAnimFinished && !attacked)
        {
            enemy.StartCoroutine(InstantiateBowProjectile());
        }

    }

    IEnumerator InstantiateBowProjectile()
    {
        attacked = true;
        yield return new WaitForSeconds(0f);
        Enemy.Instantiate(enemy.Projectile, enemy.ProjectilePosition.position, enemy.ProjectilePosition.rotation);
        stateMachine.ChangeState(enemy.idleState);
    }


}
