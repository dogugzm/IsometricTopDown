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


    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,Player.closestPosition, projectileSpeed * Time.deltaTime);
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
            GameObject a  =Instantiate(hitPrefab, Player.closestPosition + new Vector3(0, 1, 0), Quaternion.identity);
            Destroy(a, 2f);
            Destroy(gameObject);
        }
    }
}
