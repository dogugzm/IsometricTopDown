using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private Vector3 Target;

    [SerializeField] private float projectileSpeed;
    [SerializeField] protected GameObject hitPrefab;
    [SerializeField] private float projectileRotationSpeed;
    [SerializeField] private Transform projectileMesh;

    protected float maxLifeTime = 2f;
    protected float maxLifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        Target = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += projectileSpeed * Time.deltaTime * Target;
        projectileMesh.Rotate(new Vector3(0, -projectileRotationSpeed, 0));

        maxLifeTimer += Time.deltaTime;
        if (maxLifeTimer > maxLifeTime)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //It will go on interface

        

        
    }
}
