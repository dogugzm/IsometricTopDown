using DG.Tweening;
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
        //transform.DORotate(new Vector3(0, 90, 0), 0.1f).SetLoops(-1);
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.position + Player.mouseClickedDir);
        transform.Translate(Target.forward * magicBallSpeed * Time.deltaTime);
        //transform.DOMove(Player.mouseClickedDir, 2f);
        //Vector3.MoveTowards(transform.position, Player.mouseClickedDir , magicBallSpeed * Time.deltaTime);
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(PlayerAttack.mouseClickedDir), 0.1f);
        //transform.position = Vector3.MoveTowards(transform.position, Player.mouseClickedDir, magicBallSpeed * Time.deltaTime);

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
