using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public GameObject hitPoint;
    public float range;
    [Range(0,360)]
    public float angle;
    [SerializeField] LayerMask enemyLayers;
 
    private void Update()
    {
        
    }

    public void AttackToEnemies() //MARKER: Animaton Event
    {
        Collider[] hitEnemies = Physics.OverlapSphere(hitPoint.transform.position, range, enemyLayers);

        foreach (Collider enemyCollider in hitEnemies) //range i�inde 
        {
            Debug.Log(enemyCollider.name);
            Vector3 direction = enemyCollider.transform.position - Player.closestPosition;
            if (Vector3.Angle(transform.forward, direction) < angle / 2) //g�r�� alan�m�n i�inde  //tag li revize gelecek
            {
                enemyCollider.GetComponent<Enemy>().Hurt(50);
                
                //hurt animation
                //change material            
            }

        }
    }
   
}
