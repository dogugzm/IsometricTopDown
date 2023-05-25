using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class Entity : MonoBehaviour, IDamagable
{
    public FiniteStateMachine stateMachine;
    public Rigidbody rb { get; private set; }
    public Animator Anim { get; private set; }
    public NavMeshAgent agent { get; private set; }
    public Transform Target { get; private set; }
    public GameObject ParryIndiciator;

    public SkinnedMeshRenderer meshRend;

    protected float health;
    [SerializeField] protected float maxHealth;
    public float distance;
    public bool goToHurtState;
    public float damageTaken;
    public float knockBackDistance;
    public bool isAttackAnimFinished;
    public bool isHitAnimFinished;
    public bool isHit2AnimFinished;
    public bool isDriftAnimFinished;
    public bool isParryAnimFinished;
    public bool isParriable;
    //public bool isParried = false;



    [SerializeField] private LayerMask playerLayer;
    public float damage;
    public TextMeshPro showState;
    public Vector3 closestPosition;
    public int angle;

    public virtual void Start()
    {

        goToHurtState = false;
        isAttackAnimFinished = false;
        isHitAnimFinished = false;
        isHit2AnimFinished = false;
        isDriftAnimFinished = false;
        isParriable = false;
        isParryAnimFinished = false;

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
        Destroy(gameObject, time);
    }

    public virtual void ParriedState()
    {

    }

    public bool IsEnemyDead()
    {
        if (health <= 0)
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
    public void Hit2AnimFinished()
    {
        isHit2AnimFinished = true;
    }
    public void DriftAnimFinished()
    {
        isDriftAnimFinished = true;
    }
    public void SetParriableTrue()
    {
        isParriable = true;
        ParryIndiciator.SetActive(true);
        Target.GetComponent<Player>().ParriableEnemies.Add(this);
    }
    public void SetParriableFalse()
    {
        isParriable = false;
        ParryIndiciator.SetActive(false);
        Target.GetComponent<Player>().ParriableEnemies.Remove(this);
    }
    public void ParryAnimFinished()
    {
        isParryAnimFinished = true;
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
                if (playerScript.IsInDamagableState())
                {
                    Debug.Log(playerScript.StateMachine.CurrentState);
                    playerScript.damageTaken = damage;
                    playerScript.StateMachine.ChangeState(playerScript.HitState);
                    
                }

            }
        }
    }

    //TODO: damagable stateler eklenmeli ve death statinde daha fazla damage yememeliyim.


    #endregion

    #region Interface Implementation Hit

    public virtual void OnHit()
    {


    }
    public virtual void OnHitGreate()
    {


    }
    #endregion

    #region GIZMOS

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 2f);
    }


    #endregion
}
