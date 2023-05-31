using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody rb;
    

    private Transform Target;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject hitPrefab;
    private float maxLifeTime = 4f;
    private float maxLifeTimer;
    public bool goBack;
    Vector3 goBackDirection;


    // Start is called before the first frame update
    void Start()
    {
        goBack=false;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {   
        if (goBack)
        {
            //transform.Translate(goBackDirection * Time.deltaTime * projectileSpeed); 
            transform.position += goBackDirection * Time.deltaTime*(projectileSpeed+10f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,Player.closestPosition, projectileSpeed * Time.deltaTime);         
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((Player.closestPosition + new Vector3(0,1,0)) - transform.position).normalized, 0.1f);
        maxLifeTimer += Time.deltaTime;
        if (maxLifeTimer>maxLifeTime)
        {           
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HitSomething();
            other.GetComponent<Player>().StateMachine.ChangeState(other.GetComponent<Player>().HitState);
        }
        if (other.CompareTag("Enemy"))
        {
            HitSomething();
            other.GetComponent<Enemy>().Hurt();
        }
    }

    private void HitSomething()
    {
            GameObject a = Instantiate(hitPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(a, 2f);
            Destroy(gameObject);
    }

    public void GoBack()
    {
        goBack=true;
        Debug.Log("GoBack");
        goBackDirection = Target.GetComponent<Player>().desiredMoveDirection;
        //goBackDirection.y = Player.closestPosition.y;

    }
}
