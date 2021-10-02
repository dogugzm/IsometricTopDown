
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : Enemy
{
   
    bool active;

    [SerializeField] private GameObject Projectile;
    
    protected override void Start() {
        base.Start();
        active = true;
    }

    [SerializeField] private float spawnDistance;

    protected override void Move()
    {
        Rotate();
        if (!active)
        {
            return;
        }
        StartCoroutine(RandomMove());
        
    }

    IEnumerator InstantiateProjectileWithDelay(float time)
    {
        animator.SetTrigger("TisAttack");
        yield return new WaitForSeconds(time);
        Vector3 insPos = new Vector3(transform.localPosition.x,transform.localPosition.y+1,transform.localPosition.z+1.5f);
        Instantiate(Projectile, insPos, transform.rotation);      
    }

    private IEnumerator RandomMove()
    {
            active =false;
            yield return new WaitForSeconds(Random.Range(2f,7f));
            transform.position = new Vector3(Random.Range(Target.position.x + spawnDistance, Target.position.x - spawnDistance), transform.position.y, Random.Range(Target.position.z + spawnDistance, Target.position.z - spawnDistance));
            yield return new WaitForSeconds(Random.Range(1f,3f));
            StartCoroutine(InstantiateProjectileWithDelay(0.3f));
            active =true;
    } 



}
