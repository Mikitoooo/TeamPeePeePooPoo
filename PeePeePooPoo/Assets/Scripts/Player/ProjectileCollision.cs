using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public GameObject deathExplosion;
    public float damage;
    private void OnCollisionEnter(Collision collision)
    {
        // instantiante impact particle effect
        GameObject deathExplosionClone = Instantiate(deathExplosion, transform.position, transform.rotation);
        // Check if the target has a enemy health script attached
        if(collision.gameObject.GetComponent<EnemyHealth>())
        {
            //deal damage
            collision.gameObject.GetComponent<EnemyHealth>().health = collision.gameObject.GetComponent<EnemyHealth>().health - damage;
            //Call death event if the target dies
            if (collision.gameObject.GetComponent<EnemyHealth>().health <= 0)
                collision.gameObject.GetComponent<EnemyHealth>().DeathEvent();
        }
        // Destroy the projectil on contact
        Destroy(transform.parent.gameObject);
    }
}