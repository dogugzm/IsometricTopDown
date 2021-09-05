
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : Enemy
{
    private float moveRate = 2f;
    private float moveTimer;

    private float shootRate = 4.1f;
    private float shootTimer;

    

    [SerializeField] private GameObject Projectile;
    


    [SerializeField] private float spawnDistance;

    protected override void Move()
    {
        RandomMove();
        Rotate();
    }

    protected override void Attack()
    {
        base.Attack();
        shootTimer += Time.deltaTime;
        if (shootTimer>shootRate)
        {

            Instantiate(Projectile, transform.position + new Vector3(0, 1, 0), transform.rotation);
            shootTimer = 0;
        }

    }

    private void RandomMove()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer>moveRate)
        {
            WaitTime(Random.Range(3,6));
            transform.position = new Vector3(Random.Range(Target.position.x + spawnDistance, Target.position.x - spawnDistance), transform.position.y, Random.Range(Target.position.z + spawnDistance, Target.position.z - spawnDistance));
            
            moveTimer = 0;
        }
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public override void Hurt(float damage)
    {
        base.Hurt(damage);
        Debug.Log("spawner enemy hurted");
    }


}
