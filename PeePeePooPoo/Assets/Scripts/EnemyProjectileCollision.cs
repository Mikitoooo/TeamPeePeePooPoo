using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour
{
    public GameObject deathExplosion;
    public float damage;
    private void OnCollisionEnter(Collision collision)
    {
        // instantiante impact particle effect
        GameObject deathExplosionClone = Instantiate(deathExplosion, transform.position, transform.rotation);
        
        // Destroy the projectile on contact
        Destroy(transform.parent.gameObject);
    }
}
