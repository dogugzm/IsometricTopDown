using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : BaseProjectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().OnHit();
            Destroy(gameObject);
           
        }
    }
}
