using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemy : Enemy
{
    protected override void Move()
    {
        base.Move();
    }

    public override void Hurt(float damage)
    {
        base.Hurt(damage);
        Debug.Log("runner enemy hurted");
    }
}
