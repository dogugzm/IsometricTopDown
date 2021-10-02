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
 
    public void AttackToEnemies() //MARKER: Animaton Event
    {
        
        Collider[] hitEnemies = Physics.OverlapSphere(hitPoint.transform.position, range, enemyLayers);

        foreach (Collider enemyCollider in hitEnemies) 
        {
            
            Vector3 direction = enemyCollider.transform.position - Player.closestPosition;
            if (Vector3.Angle(transform.forward, direction) < angle / 2) 
            {
                if (enemyCollider.gameObject.layer==7) //enemy layer 
                {
                    
                     enemyCollider.GetComponent<Enemy>().Hurt();  //damagable interface dene!
                }
                else if (enemyCollider.gameObject.layer==9)
                {
                    enemyCollider.GetComponent<Projectile>().GoBack();    
                }
                 
                
               
                
                          
            }

        }
    }


   
}
