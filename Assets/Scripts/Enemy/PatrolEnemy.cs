
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolEnemy : Enemy
{
    
    [SerializeField] private float maxDistance;
    Vector3 randomPoint;

    protected override void Start()
    {
        base.Start();
        randomPoint = new Vector3(Random.Range(transform.position.x + maxDistance, transform.position.x - maxDistance), transform.position.y, Random.Range(transform.position.z + maxDistance, transform.position.z - maxDistance));
    }

    protected override void Move()
    {
        RandomNavmeshLocation();
    }

    public void RandomNavmeshLocation()
    {
        if (Vector3.Distance(transform.position,randomPoint)<0.2f)
        {
            agent.ResetPath();
            randomPoint = new Vector3(Random.Range(transform.position.x + maxDistance, transform.position.x - maxDistance), transform.position.y, Random.Range(transform.position.z + maxDistance, transform.position.z - maxDistance));
        }
        agent.SetDestination(randomPoint);

    }





}
