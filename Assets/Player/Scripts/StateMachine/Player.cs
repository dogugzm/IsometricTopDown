using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour,IDamagable
{
    #region ANIMATION SMOOTH VARIABLES
    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;
    #endregion

    #region ABOUT STATES
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerRollState RollState { get; private set; }
    public PlayerMeleeState MeleeState { get; private set; }
    public PlayerCombat2State Combat2State { get; private set; }
    public PlayerCombat3State Combat3State { get; private set; }
    public PlayerDashCombatState DashCombatState { get; private set; }
    public PlayerHeavyAttackState HeavyAttackState { get; private set; }
    public PlayerShootState ShootState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerParryState ParryState { get; private set; }


    #endregion

    #region COMPONENTS
    [Header("Scriptable Object")]
    [SerializeField] private PlayerData playerData;
    public Animator Anim { get; private set; }
    public CharacterController controller { get; private set; }
    public Rigidbody rb { get; private set; }
    public HealthBar healthBar;
    public EquipmentController equipmentController;
    public EffectController effectController;
    public FieldOfView fieldOfViewScript;


    #endregion

    public LayerMask layerMask;

    [HideInInspector] public float InputX;
    [HideInInspector] public float InputZ;
    [HideInInspector] public float Speed;
    [HideInInspector] public bool CanDash = true;

    [HideInInspector] public Vector3 desiredMoveDirection { get; set; }
    [HideInInspector] public Vector3 AttackInputDirection { get; set; }

    public List<Entity> ParriableEnemies;

    public Entity currentEnemy;

    [HideInInspector] public static Vector3 closestPosition;

    public GameObject MagicBall;
    public GameObject Sword;
    public Transform PorjectilePosition;
    public GameObject CinematicCamera;
    public Transform CinematicCameraFocusObject;

    public float health;
    public float maxHealth = 100;
    public float damageTaken;

    [Header("MOVEMENT")]
    public float Velocity;
    public float desiredRotationSpeed = 10f;

    private Vector3 verticalVelocity = Vector3.zero;

    public ParticleSystem SwordParticle;
    public ParticleSystem ParryParticle;

    bool chainStarted;

    public string currentStateText;
    public ParticleSystem hitEffect;

    /// <summary>
    /// Player's sword will activate or deactivate with in given seconds.
    /// </summary>
    /// <param name="delay">in seconds</param>
    /// <param name="willOpen">activate(T) or deactivate(F)</param>
    /// <returns></returns>
    public IEnumerator ActivateSwordWithDelay(float delay, bool willOpen)
    {
        SwordParticle.Stop();
        yield return new WaitForSeconds(delay);
        Sword.SetActive(willOpen);
        SwordParticle.Play();
    }

    private void Awake()
    {
        chainStarted = false;
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "IDLE");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "MOVE");
        RollState = new PlayerRollState(this, StateMachine, playerData, "ROLL");
        MeleeState = new PlayerMeleeState(this, StateMachine, playerData, "MELEE");
        Combat2State = new PlayerCombat2State(this, StateMachine, playerData, "COMBAT2");
        Combat3State = new PlayerCombat3State(this, StateMachine, playerData, "COMBAT3");
        ShootState = new PlayerShootState(this, StateMachine, playerData, "SHOOT");
        HitState = new PlayerHitState(this, StateMachine, playerData, "HIT");
        DashCombatState = new PlayerDashCombatState(this, StateMachine, playerData, "DASH_ATTACK");
        HeavyAttackState = new PlayerHeavyAttackState(this, StateMachine, playerData, "HEAVY_ATTACK");
        ParryState = new PlayerParryState(this, StateMachine, playerData, "PARRY");

    }

    private void Start()
    {
        Sword.SetActive(false);
        Anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        StateMachine.Initialize(IdleState);
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicalUpdate();
        InputMagnitude();
        VerticalMovement();
        RotationJob();
        FindMeshPosition();
        ChangeAttackInputDirection();
        SetCurrentEnemy();
    }

    private void SetCurrentEnemy()
    {
        RaycastHit info;
        if (Physics.SphereCast(transform.position, 0.5f, AttackInputDirection, out info, 5, layerMask))
        {
            if (!info.collider.gameObject.GetComponent<Entity>().isDeath)
            {
                currentEnemy = info.collider.gameObject.GetComponent<Entity>();
            }
        }
    }

    public void ClearCurrentEnemyIfSame(Entity enemy)
    {
        if (currentEnemy == enemy)
        {        
            currentEnemy = null;    
        }
    }

    public Vector3 TargetOffset(Transform target)
    {
        Vector3 position;
        position = target.GetComponent<Entity>().closestPosition;
        return Vector3.MoveTowards(position, closestPosition, 0.9f);
    }

    public void CameraShake(float effectSize)
    {
        CinemachineShake.instance.ShakeCamera(effectSize, 0.2f);
    }

    #region IN HERE METHODS

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //Calculate the Input Magnitude for animation blend.
        Speed = new Vector2(InputX, InputZ).normalized.sqrMagnitude;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed *= 2;
        }

    }

    private void VerticalMovement()
    {
        if (controller.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = 0;
        }
        verticalVelocity.y += -9.81f * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
        
    }

    private void RotationJob()
    {
        if (desiredMoveDirection == Vector3.zero)
        {
            return;

        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }

    private void FindMeshPosition() //update den çýkar gerektiðinde çaðýr.
    {
        NavMeshHit hit;

        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            closestPosition = hit.position;
        }


    }



    #endregion

    #region Animation Triggers
    private void RollAnimationFinishTrigger()
    {
        StateMachine.CurrentState.RollAnimationFinishTrigger();
    }
    private void MeleeAnimationFinishTrigger()
    {
        StateMachine.CurrentState.MeleeAnimationFinishTrigger();
    }
    private void Combat2AnimationFinishTrigger()
    {
        StateMachine.CurrentState.Combat2AnimationFinishTrigger();
    }
    private void Combat3AnimationFinishTrigger()
    {
        //Debug.Log("animation finished");
        StateMachine.CurrentState.Combat3AnimationFinishTrigger();
    }
    private void DashAttackAnimationFinishTrigger()
    {
        StateMachine.CurrentState.DashAttackAnimationFinishTrigger();
    }
    private void HeavyAttackAnimationFinishTrigger()
    {
        StateMachine.CurrentState.HeavyAttackAnimationFinisTrigger();

    }
    private void HitAnimationFinishTrigger()
    {
        StateMachine.CurrentState.HitAnimationFinisTrigger();
    }
    private void ParryAnimationFinishTrigger()
    {
        StateMachine.CurrentState.ParryAnimationFinisTrigger();
    }

    public void ParryParticlePlay()
    {
        StartCoroutine(ParryEffect());
    }

    IEnumerator ParryEffect()
    {
        ParryParticle.Play();
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.05f);
        Time.timeScale = 1f;

    }

    #endregion

    public void ChangeRotationToCursor()
    {
        desiredMoveDirection = (Cursor.instance.pointToLook - closestPosition).normalized;
    }

    public void ChangeAttackInputDirection()
    {
        AttackInputDirection = (Cursor.instance.pointToLook - closestPosition).normalized;
    }

    public bool IsInDamagableState()
    {
        if (StateMachine.CurrentState == RollState ||
            StateMachine.CurrentState == HitState ||
            StateMachine.CurrentState == ParryState)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnGUI()
    {
        GUIStyle headStyle = new GUIStyle();
        headStyle.fontSize = 50;
        GUI.color = Color.blue;
        GUI.Label(new Rect(10, 10, 300, 50), currentStateText, headStyle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, AttackInputDirection);
        //Gizmos.DrawWireSphere(transform.position, 1);
        if (currentEnemy != null)
            Gizmos.DrawSphere(currentEnemy.transform.position, 1f);      
    }

    public void OnHit()
    {
        hitEffect.Play();
        health -= damageTaken;
        healthBar.SetHealth(health);
    }

    public void OnHitGreate()
    {
        throw new System.NotImplementedException();
    }
}
