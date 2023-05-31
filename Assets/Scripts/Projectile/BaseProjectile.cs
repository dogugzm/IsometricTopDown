using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private Vector3 Target;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject hitPrefab;
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

    private void OnTriggerEnter(Collider other)
    {
        //It will go on interface

        if (other.CompareTag("Enemy"))
        {
            GameObject a = Instantiate(hitPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(a, 2f);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Player"))
        {
            GameObject a = Instantiate(hitPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(a, 2f);
            Destroy(gameObject);
            //Destroy(other.gameObject);
        }
    }
}
