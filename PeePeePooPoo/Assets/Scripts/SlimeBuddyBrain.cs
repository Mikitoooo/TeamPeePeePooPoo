using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBuddyBrain : MonoBehaviour
{
    public Transform spawnLocation;
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate;
    public float fireRateEv;
    public float fireRateIncrease;
    bool canShoot = true;

    public float radius;
    private EnemyHealth closestEnemy;

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                // Check if this enemy is closer than the previous closest enemy
                if (closestEnemy == null || Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, closestEnemy.transform.position))
                {
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null)
        {
            // The closest enemy has been found, do something with it
            //Debug.Log("Closest enemy detected: " + closestEnemy.gameObject.name);
            transform.LookAt(closestEnemy.transform);
            if (canShoot)
            {
                FireProjectile(closestEnemy.transform.position);
                StartCoroutine(ResetFire(fireRate));
                canShoot = false;
            }
        }

    }

    public void ImproveFireRate()
    {
        fireRateEv = fireRateEv + fireRateIncrease;

        fireRate = Mathf.Pow(fireRate, fireRateEv);

    }

    void FireProjectile(Vector3 targetPoint)
    {
        //get the direction
        Vector3 direction = (targetPoint - spawnLocation.transform.position).normalized;

        //instantiate projectile
        GameObject projectileInstance = Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation);

        // Add damage to projectile
        projectileInstance.transform.GetChild(0).GetComponent<ProjectileCollision>().damage = PlayerShoot.instance.damage/2;

        // Get the Rigidbody component of the projectile instance
        Rigidbody rb = projectileInstance.transform.GetChild(0).GetComponent<Rigidbody>();

        // Add force to the projectile
        rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);

    }

    private IEnumerator ResetFire(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }
}
