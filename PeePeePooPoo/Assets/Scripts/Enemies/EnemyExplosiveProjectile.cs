using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosiveProjectile : MonoBehaviour
{
    public GameObject deathExplosion;
    public Rigidbody rb;
    public float damage;
    public float explosionDamage;
    [Header("Explosive")]
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

            if (collision.gameObject.GetComponent<PlayerStats>().currentHealth <= 0)
            {
                UIController.instance.PlayerDiedUI();
            }
        }
        SoundsManager.instance.PlayerTakesDamage();
        // stop projectile on contact
        rb.constraints = RigidbodyConstraints.FreezePosition;
        // Start explosion
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        explosiveGrowth1.DOScale(new Vector3(growSize, growSize, growSize), 1);
        yield return new WaitForSeconds(1);
        explosiveGrowth2.DOScale(new Vector3(growSize, growSize, growSize), 1);
        yield return new WaitForSeconds(1);
        explosiveGrowth3.DOScale(new Vector3(growSize, growSize, growSize), 1);
        yield return new WaitForSeconds(1);

        // instantiante impact particle effect
        GameObject deathExplosionClone = Instantiate(deathExplosion, transform.position, transform.rotation);
        // add damage to explosion
        deathExplosionClone.GetComponent<EnemyExplosion>().damage = explosionDamage;
        // Destroy the projectile on contact
        Destroy(transform.gameObject);
    }
}
