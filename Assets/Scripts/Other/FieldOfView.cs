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
                if (enemyCollider.gameObject.layer==7)
                {
                     //enemyCollider.GetComponent<Entity>().goToHurtState = true;
                     enemyCollider.GetComponent<Enemy>().Hurt();
                }
                else
                {
                    
                    enemyCollider.GetComponent<Projectile>().GoBack(transform.forward);    
                }   
                
               
                
                          
            }

        }
    }


   
}
