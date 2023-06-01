public class EBow_MoveState : MoveState
{
    private EnemyBow enemy;

    public EBow_MoveState(Entity entity, FiniteStateMachine stateMachine, EnemyBow enemy, string name) : base(entity, stateMachine, name)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.ResetPath();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.LookAtPlayer();
        if (enemy.GetDistanceBetweenPlayer() > 15)
        {
            enemy.agent.destination += enemy.GetDirectionToPlayer();
        }
        else
        {
            stateMachine.ChangeState(enemy.attackState);
            return;
        }
    }
}
