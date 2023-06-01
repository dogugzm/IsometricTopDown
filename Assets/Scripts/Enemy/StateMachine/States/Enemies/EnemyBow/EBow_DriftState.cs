using UnityEngine;

public class EBow_DriftState : IdleState
{
    private EnemyBow enemy;
    Vector3 tempDirection;


    public EBow_DriftState(Entity entity, FiniteStateMachine stateMachine, EnemyBow enemy, string name) : base(entity, stateMachine, name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.ResetPath();
        enemy.PlayHitParticle();
        enemy.DecreaseHealth(enemy.damageTaken);
        enemy.isDriftAnimFinished = false;
        enemy.Anim.SetBool("isHitBig", true);
        tempDirection = enemy.GetDirectionToPlayer();

    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.ResetPath();
        enemy.Anim.SetBool("isHitBig", false);


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.LookAtPlayer();

        enemy.agent.Move(tempDirection * -10f * Time.deltaTime);



        if (enemy.isDriftAnimFinished)
        {
            if (enemy.IsEnemyDead())
            {
                stateMachine.ChangeState(enemy.deathState);

            }
            else
            {
                stateMachine.ChangeState(enemy.idleState);
            }

        }




    }


}
