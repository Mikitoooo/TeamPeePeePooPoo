using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosiveProjectileExplosion : MonoBehaviour
{
    public GameObject deathExplosion;
    public Rigidbody rb;
    public float damage;
    [Header("Explosive")]
    public Transform explosiveGrowth1;
    public Transform explosiveGrowth2;
    public Transform explosiveGrowth3;
    public float growSize;
    bool hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hit)
        {
            print(collision.gameObject.name);
            //// Stop linear velocity
            //rb.velocity = Vector3.zero;
            //// Stop angular velocity
            //rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePosition;

            StartCoroutine(Explosion());

            hit = true;
        }

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
        // Destroy the projectile on contact
        Destroy(transform.gameObject);
    }
}
