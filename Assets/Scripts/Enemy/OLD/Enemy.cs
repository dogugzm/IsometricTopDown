using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    
    [SerializeField] private string enemyName;
    [SerializeField] private float maxHealth;
    [SerializeField] private float distance;
    [SerializeField]private SkinnedMeshRenderer meshRenderer;
    [SerializeField] protected ParticleSystem hitParticle;
    [SerializeField] private bool isVisibleOnStart=false;

    private float health;
    bool knockBack;
    protected float damage = 40f;
    protected private Transform Target; //it is player
    protected NavMeshAgent agent;
    protected Animator animator;

    private void Awake() {
        this.gameObject.SetActive(isVisibleOnStart);
        
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
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
        
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        meshRenderer.material.EnableKeyword("_EMISSION");                     
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
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
        
    }
    protected virtual void Death()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            return;
        }
        animator.SetTrigger("TisDeath");
        Destroy(gameObject,1f);
    }
    public virtual void Hurt()
    {
        StartCoroutine(KnockBack());  //TODO:2_KnockBack
        hitParticle.Play();  //TODO:5_Particle
        animator.SetTrigger("TisHit");
        health -= damage;
        
    }
    public virtual void Spawn()
    {
        //spawn instantiate effect
        Debug.Log("Activated");
        this.gameObject.SetActive(true);
    }
}
