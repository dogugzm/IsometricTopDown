using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    [SerializeField] private GameObject MagicBall;
    
    private float shootRate = 2f;
    private float shootTimer;

    
    private float meleeRate = 1f;
    private float meleeTimer;

    bool magicState;

    [HideInInspector] public static Vector3 closestPosition; 

    [HideInInspector] public static Vector3 mouseClickedDir;
    MovementInput movementScript;

    // Start is called before the first frame update
    void Start()
    {
        magicState = false;
        movementScript = GetComponent<MovementInput>();
        anim = GetComponent<Animator>();
        shootTimer = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        FindMeshPosition();

        MeleeAttack();
        MagicAttack();
    }

    private void FindMeshPosition()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            closestPosition = hit.position;
        }
    }

    private void MeleeAttack()
    {
        meleeTimer += Time.deltaTime;
        if (meleeTimer>=meleeRate)
        {
            if (Input.GetMouseButtonDown(0) && !magicState)
            {
                meleeTimer = 0;
                movementScript.ChangeRotationToCursor();
                CinemachineShake.instance.ShakeCamera(1f, 0.5f);
                anim.SetTrigger("isAttacking");
            }
        }
       
    }


    #region MAGIC
    private void MagicAttack()
    {

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootRate)
        {
            MagicBallInstantiate();

        }
    }

    private void MagicBallInstantiate()
    {
        
        if (Input.GetMouseButton(1))
        {
            magicState = true;
            if (Input.GetMouseButtonDown(0))
            {
                mouseClickedDir = transform.forward;
                closestPosition.y = closestPosition.y + 1;              
                Instantiate(MagicBall, closestPosition, Quaternion.identity);
                shootTimer = 0;
                magicState = false;
            }        
        }
        if (Input.GetMouseButtonUp(1))
        {
            magicState = false;
        }
    }
    #endregion
}
