using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public float damage = 10;

    bool hit = false;

    private void Awake()
    {
        explosionRadius = this.GetComponent<SphereCollider>().radius;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hit)
        {
            // Apply force to the player
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                print("I FOUND RIGIDBODY");
            }

            // Apply damage to the player (assuming the player has a Health script)
            PlayerStats playerHealth = other.GetComponent<PlayerStats>();
            if (playerHealth != null)
            {
                SoundsManager.instance.PlayerTakesDamage();
                playerHealth.currentHealth -= damage;
                UIController.instance.UpdateHealthBar();
            }

            hit = true;
        }
    }
}
