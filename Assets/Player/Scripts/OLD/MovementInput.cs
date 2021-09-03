
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementInput : MonoBehaviour {
  

    public float Velocity;
    [Space]
	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public bool useMouseRotation;
	public float desiredRotationSpeed = 10f;
	public Animator anim;
	public float Speed;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
   
	Rigidbody rb;
  

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    CharacterController controller;
    Vector3 verticalVelocity;
    bool canMove;

	// Use this for initialization
	void Start () 
    {
        canMove = true;
        controller = GetComponent<CharacterController>();       
        useMouseRotation = false;
		rb = GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;      
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        InputMagnitude();
        
        RotationJob();
        Roll();
        VerticalMovement();

        if (Input.GetMouseButton(1))
        {
            useMouseRotation = true;
            canMove = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            useMouseRotation = false;
            canMove = true;
        }
        if (useMouseRotation)
        {
            ChangeRotationToCursor();
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);      
    }

    public void ChangeRotationToCursor()
    {
        desiredMoveDirection = (Cursor.instance.pointToLook - PlayerAttack.closestPosition).normalized;
    }
   
    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AnimatorStateInfo animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animatorStateInfo.IsName("Rolling") || Speed < allowPlayerRotation) //duruyosam veya hali hazýrda animasyon oynuyosa
            {
                return;
            }
            rb.AddForce(transform.forward, ForceMode.Force);
            anim.SetTrigger("isRolling");
            StartCoroutine(VolumeTest.instance.RollEffect());
        }
    } 

    void MoveInputs() {
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

		var camera = Camera.main;
		var forward = cam.transform.forward;
		var right = cam.transform.right;

		forward.y = 0f;
		right.y = 0f;

		forward.Normalize ();
		right.Normalize ();

		desiredMoveDirection = forward * InputZ + right * InputX;
        controller.Move(desiredMoveDirection * Time.deltaTime * Velocity);

    }  

    void InputMagnitude() {
		//Calculate Input Vectors
		InputX = Input.GetAxis ("Horizontal");
		InputZ = Input.GetAxis ("Vertical");

		//Calculate the Input Magnitude
		Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (!canMove)  //hareket etmemem gereken yerde anim oynatma
        {
            Speed = 0;
        }
        
        if (Speed > allowPlayerRotation)
        {
                anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
                MoveInputs();


        }
        else if (Speed < allowPlayerRotation)
        {
                anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);

        }
        
	
	}

    #region Not Using
    public void LookAt(Vector3 pos)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
    }

    public void RotateToCamera(Transform t)
    {

        var camera = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        desiredMoveDirection = forward;

        t.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
    }
    #endregion
}
