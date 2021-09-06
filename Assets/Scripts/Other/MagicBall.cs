using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MagicBall : MonoBehaviour
{
    

    private Transform Target;
    [SerializeField] private float magicBallSpeed;
    [SerializeField] private GameObject hitPrefab;

    private float maxLifeTime = 2f;
    private float maxLifeTimer;
    Vector3 mouseClickedPos;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.Translate(Player.mouseClickedDir * magicBallSpeed * Time.deltaTime);
       // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerAttack.mouseClickedDir), 0.1f);


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
