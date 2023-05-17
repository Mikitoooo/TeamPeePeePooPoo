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
        print(collision.gameObject.name);

        if (explosive == false)
        {
            // instantiante impact particle effect
            GameObject deathExplosionClone = Instantiate(deathExplosion, transform.position, transform.rotation);

            // Destroy the projectile on contact
            Destroy(transform.parent.gameObject);
        } 
        else if(explosive == true)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            if (hit == false)
            {
                hit = true;
                StartCoroutine(Explosion());
            }
        }
    }

    IEnumerator Explosion()
    {
        explosiveGrowth1.DOScale(new Vector3(growSize, growSize, growSize), 1);
        yield return new WaitForSeconds(2);
        explosiveGrowth2.DOScale(new Vector3(growSize, growSize, growSize), 1);
        yield return new WaitForSeconds(2);
        explosiveGrowth3.DOScale(new Vector3(growSize, growSize, growSize), 1);
        yield return new WaitForSeconds(2);

        // instantiante impact particle effect
        GameObject deathExplosionClone = Instantiate(deathExplosion, transform.position, transform.rotation);
        // Destroy the projectile on contact
        Destroy(transform.parent.gameObject);
    }
}
