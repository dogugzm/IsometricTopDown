using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UIElements;


public class Entity : MonoBehaviour , IDamagable
{
    public FiniteStateMachine stateMachine;
    public Rigidbody rb { get; private set; }
    public Animator Anim { get; private set; }
    public NavMeshAgent agent { get; private set; }
    public Transform Target { get; private set; }

    public SkinnedMeshRenderer meshRend;

    protected float health;
    [SerializeField] protected float maxHealth;
    public float distance;
    public bool goToHurtState;
    public float damageTaken;
    public float knockBackDistance;
    public bool isAttackAnimFinished;
    public bool isHitAnimFinished;
    [SerializeField] private LayerMask playerLayer;
    public float damage;
    public TextMeshPro showState;
    private Vector3 closestPosition;
    public int angle;

    public virtual void Start()
    {        
        goToHurtState = false;
        isAttackAnimFinished = false;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
        FindMeshPosition();
        Anim.SetFloat("Blend", agent.velocity.magnitude / agent.speed);
    }

    public float GetDistanceBetweenPlayer()
    {
        return Vector3.Distance(closestPosition, Player.closestPosition);
    }

    public Vector3 GetDirectionToPlayer()
    {
        return (Player.closestPosition - closestPosition).normalized;
    }

    public void DecreaseHealth(float damage) 
    {
        health -= damage;
    }

    public void DestroyMe(float time)
    {
        Destroy(gameObject,time);
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

    public bool IsLastHit()
    {
        if (health <= damage)
        {
            return true;
        }

        return false;
    }

    public bool IsEnemyHasReachedDestiantion()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
            return false;

            }
        }
        else
        {
            return false;
        }
    }

    public void LookAtPlayer()
    {
        var lookrotation = Quaternion.LookRotation(GetDirectionToPlayer()); //this will automatically keep 'up' up, but if you want to rotate around another axis you can add it
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookrotation, 150f * Time.deltaTime);
    }

    public Vector3 TargetOffset(Transform target)
    {
        Vector3 position;
        position = target.position;
        return Vector3.MoveTowards(position, closestPosition, .75f);
    }

    private void FindMeshPosition() //update den çýkar gerektiðinde çaðýr.
    {
        NavMeshHit hit;

        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            closestPosition = hit.position;
        }
    }

    public IEnumerator ChangeStateInSeconds(float time, State nextState)
    {
        yield return new WaitForSeconds(time);
        stateMachine.ChangeState(nextState);
    }

    public IEnumerator WaitTime(float time) {
        yield return new WaitForSeconds(time);
    }

    public void ShowState(string a)
    {
        showState.text = a;
    }

    #region Animation Event
    
    public void AttackAnimFinished()
    {
        isAttackAnimFinished = true;
        
    }

    public void HitAnimFinished()
    {
        isHitAnimFinished = true;
    }


    public void CheckPlayerIfInsideAttackRange()// animation event
    {
        
        Collider[] players = Physics.OverlapSphere(transform.position, 4f, playerLayer);

        foreach (Collider player in players) //range içinde 
        {
            Vector3 direction = Player.closestPosition - closestPosition;

            if (Vector3.Angle(transform.forward, direction) < angle / 2)
            {
                Player playerScript = player.GetComponent<Player>();
                if (playerScript.StateMachine.CurrentState== playerScript.RollState || playerScript.StateMachine.CurrentState == playerScript.HitState || playerScript.StateMachine.CurrentState == playerScript.MeleeState)
                {
                    return;
                }
                playerScript.damageTaken = damage;

                playerScript.StateMachine.ChangeState(playerScript.HitState);
                
            }
        }
    }

   


    #endregion


    public virtual void OnHit()
    {


    }
    public virtual void OnHitGreate()
    {


    }

    #region GIZMOS

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 2f);
    }


    #endregion
}
