using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public Rigidbody rb { get; private set; }
    public Animator anim { get; private set; }
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
    [SerializeField] private LayerMask playerLayer;
    public float damage;
    public TextMeshProUGUI showState;



    public virtual void Start()
    {
        goToHurtState = false;
        isAttackAnimFinished = false;
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

    public void CheckPlayerIfInsideAttackRange()
    {
        
        Collider[] players = Physics.OverlapSphere(transform.position, 2f, playerLayer);

        foreach (Collider player in players) //range i?inde 
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
    #endregion

    #region GIZMOS

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

    #endregion
}
