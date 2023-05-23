using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileCollision : MonoBehaviour
{
    public GameObject deathExplosion;
    public float damage;
    [Header("Explosive")]
    public bool explosive = false;
    public Transform explosiveGrowth1;
    public Transform explosiveGrowth2;
    public Transform explosiveGrowth3;
    public float growSize;
    bool hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && hit == false)
        {
            // Damage the player
            collision.gameObject.GetComponent<PlayerStats>().currentHealth -= damage;
            UIController.instance.UpdateHealthBar();
            hit = true;
        }

        // instantiante impact particle effect
        GameObject deathExplosionClone = Instantiate(deathExplosion, transform.position, transform.rotation);
        // Destroy the projectile on contact
        Destroy(transform.gameObject);
    }
}
