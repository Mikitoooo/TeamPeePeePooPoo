using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public EnemyStats damageRef;
    private GameObject player;
    public GameObject projectile;
    public Transform spawnLocation;

    public float shootDistance;
    public float projectileSpeed;
    bool canShoot = false;
    public float rateOfFireMin;
    public float rateOfFireMax;
    public float deviationAmount;
    public int shotsFired;
    public LayerMask layerMask;

    public Vector3 projectileBuildUpSize;
    public Transform projectileBuildUpObject;

    // Start is called before the first frame update
    void Start()
    {
        // Reference the builder
        player = PlayerStats.instance.gameObject;
        // Set projectile build up to 0 scale
        projectileBuildUpObject.localScale = new Vector3(0, 0, 0);
        // start the projectile Build up
        StartCoroutine(ResetFire(Random.Range(rateOfFireMin, rateOfFireMax)));
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        //Check if Slime buddy has a line of sight to the targeted enemy
        RaycastHit HitInfo;
        Physics.Raycast(transform.position, direction, out HitInfo, 30, layerMask);
        Debug.DrawRay(transform.position, direction * 30, Color.yellow);
        if (HitInfo.collider != null)
        {
            if (HitInfo.collider.gameObject.tag == "Player")
            {
                if (Vector3.Distance(this.transform.position, player.transform.position) < shootDistance && canShoot)
                {
                    StartCoroutine(FireShot());
                }
            }
        }
    }

    void FireProjectile(Vector3 targetPoint)
    {
        // Projectile Build up
        projectileBuildUpObject.localScale = new Vector3(0, 0, 0);

        //get the direction
        Vector3 direction = (targetPoint - spawnLocation.transform.position).normalized;

        // Add random deviation to the direction
        Vector3 randomDirection = Quaternion.Euler(Random.Range(-deviationAmount, deviationAmount), Random.Range(-deviationAmount, deviationAmount), 0f) * direction;

        //instantiate projectile
        GameObject projectileInstance = Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation);

        // Get the Rigidbody component of the projectile instance
        Rigidbody rb = projectileInstance.transform.GetComponent<Rigidbody>();

        // Add force to the projectile
        rb.AddForce(randomDirection * projectileSpeed, ForceMode.Impulse);

        // Play sound effect
        SoundsManager.instance.EnemyShoot(this.GetComponent<AudioSource>());

        // Add damage to the projectile
        if(projectileInstance.GetComponent<EnemyProjectileCollision>() != null)
        {
            projectileInstance.GetComponent<EnemyProjectileCollision>().damage = damageRef.damage;
        } 
        else if(projectileInstance.GetComponent<EnemyExplosiveProjectile>() != null)
        {
            projectileInstance.GetComponent<EnemyExplosiveProjectile>().damage = damageRef.damage;
            projectileInstance.GetComponent<EnemyExplosiveProjectile>().explosionDamage = damageRef.damage * 2;
        }
    }

    IEnumerator FireShot()
    {
        if (shotsFired == 1)
        {
            FireProjectile(player.transform.position);
            StartCoroutine(ResetFire(Random.Range(rateOfFireMin, rateOfFireMax)));
            canShoot = false;
        }
        else if (shotsFired > 1)
        {
            canShoot = false;
            for (int i = 0; i < shotsFired; i++)
            {
                FireProjectile(player.transform.position);
                yield return new WaitForSeconds(0.5f);
            }
            StartCoroutine(ResetFire(Random.Range(rateOfFireMin, rateOfFireMax)));
        }
    }

    private IEnumerator ResetFire(float waitTime)
    {
        projectileBuildUpObject.DOScale(projectileBuildUpSize, waitTime);
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }
}
