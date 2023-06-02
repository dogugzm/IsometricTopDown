using UnityEngine;

public class MagicBall : BaseProjectile
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Enemy"))
        {
            GameObject a = Instantiate(hitPrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(a, 2f);
            Destroy(gameObject);
            //TODO: Do not destroy just damage
            Destroy(other.gameObject);
        }
    }



}
