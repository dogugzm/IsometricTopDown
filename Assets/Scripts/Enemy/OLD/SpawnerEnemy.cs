
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
        animator.ResetTrigger("TisAttack");
        shootTimer += Time.deltaTime;
        if (shootTimer>shootRate)
        {
            animator.SetTrigger("TisAttack");
            StartCoroutine(InstantiateProjectileWithDelay(0.3f));
            shootTimer = 0;
        }

    }

    IEnumerator InstantiateProjectileWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(Projectile, transform.position + new Vector3(0, 1, 0), transform.rotation);
        
    }

    private void RandomMove()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer>moveRate)
        {
            transform.position = new Vector3(Random.Range(Target.position.x + spawnDistance, Target.position.x - spawnDistance), transform.position.y, Random.Range(Target.position.z + spawnDistance, Target.position.z - spawnDistance));
            moveTimer = 0;
        }
    }



}
