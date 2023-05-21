using UnityEngine;

public class E1_DriftState : IdleState
{
    private Enemy1 enemy;

    public E1_DriftState(Entity entity, FiniteStateMachine stateMachine, Enemy1 enemy, string name) : base(entity, stateMachine, name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.ResetPath();
        enemy.PlayHitParticle();
        enemy.DecreaseHealth(enemy.damageTaken);
        enemy.Anim.SetBool("isHitBig", true);

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
        enemy.agent.Move(enemy.GetDirectionToPlayer() * -5f * Time.deltaTime);

        if (enemy.isDriftAnimFinished)
        {
            stateMachine.ChangeState(enemy.idleState);

        }




    }


}
