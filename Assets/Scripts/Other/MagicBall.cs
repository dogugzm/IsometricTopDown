using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System.Security.Cryptography;


public class MagicBall : MonoBehaviour
{
    
    private Vector3 Target;
    [SerializeField] private float magicBallSpeed;
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private float magicBallRotSpeed;

    private float maxLifeTime = 2f;
    private float maxLifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        Target = transform.forward;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += magicBallSpeed * Time.deltaTime * Target;
        transform.GetChild(0).Rotate(new Vector3(0, -magicBallRotSpeed, 0));    

        maxLifeTimer += Time.deltaTime;
        if (maxLifeTimer > maxLifeTime)
        {
            
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject a = Instantiate(hitPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(a, 2f);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
