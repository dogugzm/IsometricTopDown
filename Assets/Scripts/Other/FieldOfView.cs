using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public GameObject hitPoint;
    [HideInInspector]public float range;  
    
    [HideInInspector]public float angle;

    [SerializeField]private float rangeNormal;  //2.92
    [Range(0,360)]         //109
    [SerializeField]private float angleNormal;

    [SerializeField]private float rangeCombat3;  
    [Range(0,360)]         
    [SerializeField]private float angleCombat3;


    [SerializeField] LayerMask enemyLayers;
   
    private void Start() {

       range = rangeNormal;
       angle = angleNormal;   
    }
 
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
                    enemyCollider.TryGetComponent<IDamagable>(out IDamagable damagableEnemy);
                    damagableEnemy.OnHit();
                }
                else if (enemyCollider.gameObject.layer==9)
                {
                    enemyCollider.GetComponent<Projectile>().GoBack();    
                }                                                     
                          
            }

        }
    }

    public void Combat3ValuesActivate()
    {
        range = rangeCombat3;
        angle = angleCombat3;
    }

    public void GoDefaultValues()
    {
            range = rangeNormal;
            angle = angleNormal;
    }




   
}
