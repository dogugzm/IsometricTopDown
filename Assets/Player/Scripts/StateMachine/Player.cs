using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player : MonoBehaviour
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
    public PlayerShootState ShootState { get; private set; }
    public PlayerHitState HitState { get; private set; }


    #endregion

    #region COMPONENTS
    [Header("Scriptable Object")]
    [SerializeField] private PlayerData playerData;
    public Animator Anim { get; private set; }
    public CharacterController controller { get; private set; }
    public Rigidbody rb { get; private set; }


    #endregion
    [HideInInspector]public float InputX;
    [HideInInspector]public float InputZ;
    [HideInInspector] public float Speed;
    [HideInInspector] public Vector3 desiredMoveDirection {get;  set;}

    [HideInInspector] public static Vector3 closestPosition;

    [HideInInspector] public static Vector3 mouseClickedDir;
    public GameObject MagicBall;

    public float health;
    public float maxHealth = 100;
    public float damageTaken;
    [Header("MOVEMENT")]
    public float Velocity;
    public float desiredRotationSpeed = 10f;

    private Vector3 verticalVelocity;

    public ParticleSystem SwordParticle;
    

    private void Awake()
    {
        
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData,"IDLE");
        MoveState = new PlayerMoveState(this, StateMachine, playerData,"MOVE");
        RollState = new PlayerRollState(this, StateMachine, playerData, "ROLL");
        MeleeState = new PlayerMeleeState(this, StateMachine, playerData, "MELEE");
        ShootState = new PlayerShootState(this, StateMachine, playerData, "SHOOT");
        HitState = new PlayerHitState(this, StateMachine, playerData, "HIT");

    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        health = maxHealth;
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        
        InputMagnitude();
        VerticalMovement();
        RotationJob();
        FindMeshPosition();
        StateMachine.CurrentState.LogicalUpdate();
        
    }

    #region IN HERE METHODS

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //Calculate the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;
        
        

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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }
    private void FindMeshPosition()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            closestPosition = hit.position;
        }
    }



    #endregion

    #region OTHER SCRIPT METHODS
    private void RollAnimationFinishTrigger()
    {
        StateMachine.CurrentState.RollAnimationFinishTrigger();
    }  //MARKER: Animation Event
    private void MeleeAnimationFinishTrigger()
    {
        StateMachine.CurrentState.MeleeAnimationFinishTrigger();
    }  //MARKER: Animation Event

    public void ChangeRotationToCursor()
    {
        desiredMoveDirection = (Cursor.instance.pointToLook - closestPosition).normalized;
    }

    


    #endregion



}
