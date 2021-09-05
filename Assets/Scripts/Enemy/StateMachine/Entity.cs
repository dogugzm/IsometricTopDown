using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public Rigidbody rb { get; private set; }
    public Animator anim { get; private set; }
    public NavMeshAgent agent { get; private set; }

   public Transform Target { get; private set; }

    protected float health;
    [SerializeField] protected float maxHealth;
    public float distance;
    public bool goToHurtState;
    public float damageTaken;




    public virtual void Start()
    {
        goToHurtState = false;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public float GetDistanceBetweenPlayer()
    {
        return Vector3.Distance(transform.position, Target.position);
    }

    public void DecreaseHealth(float damage) 
    {
        health -= damage;
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    public bool IsEnemyDead() 
    {
        if (health<=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
