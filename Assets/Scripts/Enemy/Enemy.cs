using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    
    [SerializeField] private string enemyName;
    
    private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float distance;
    protected NavMeshAgent agent;
    bool knockBack;


    [SerializeField] protected private Transform Target; //it is player

    protected virtual void Start()
    {
        knockBack = false;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
        
    }
    private void FixedUpdate()
    {
        Move();
        if (knockBack)
        {
            agent.velocity = Target.transform.forward * 3;
        }
       
    }

    private void Update()
    {
        
        if (health<=0)
        {
            Death();
        }
        Attack();
        
    }

    protected virtual void Move()
    {
        if (Vector3.Distance(transform.position, Target.position) < 0.2f)
        {
            agent.ResetPath();
            return;
            
        }
        if (Vector3.Distance(transform.position,Target.position)<distance)
        {
            agent.SetDestination(Target.position);
            
            
        }
        
    }

    IEnumerator KnockBack()
    {
        if (agent==null)
        {
            yield break;
        }
        knockBack = true;
        agent.speed = 50;
        agent.angularSpeed = 0;
        agent.acceleration = 30;
        

        yield return new WaitForSeconds(0.2f);

        if (agent == null)
        {
            yield break;
        }
        //reset default
        knockBack = false;
        agent.speed = 3.5f;
        agent.angularSpeed = 120;
        agent.acceleration = 8;
       

    }

    protected  void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.position-transform.position), 2f);
    }

    protected virtual void Attack()
    {
        Debug.Log(enemyName + " is attacked");
    }
    protected virtual void Death()
    {
        Destroy(gameObject);
    }
    public virtual void Hurt(float damage)
    {
        StartCoroutine(KnockBack());
        health -= damage;
    }
}
