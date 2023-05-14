using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    private GameObject player;
    public GameObject projectile;
    public Transform spawnLocation;

    public float shootDistance;
    public float projectileSpeed;
    bool canShoot = false;
    public float rateOfFireMin;
    public float rateOfFireMax;

    public Vector3 projectileBuildUpSize;
    public Transform projectileBuildUpObject;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerStats.instance.gameObject;
        projectileBuildUpObject.localScale = new Vector3(0, 0, 0);
        // start the projectile Build up
        StartCoroutine(ResetFire(Random.Range(rateOfFireMin, rateOfFireMax)));
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < shootDistance && canShoot)
        {
            FireProjectile(player.transform.position);
            StartCoroutine(ResetFire(Random.Range(rateOfFireMin,rateOfFireMax)));
            canShoot = false;
        }
    }

    void FireProjectile(Vector3 targetPoint)
    {
        // Projectile Build up
        projectileBuildUpObject.localScale = new Vector3(0, 0, 0);

        //get the direction
        Vector3 direction = (targetPoint - spawnLocation.transform.position).normalized;

        //instantiate projectile
        GameObject projectileInstance = Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation);

        // Get the Rigidbody component of the projectile instance
        Rigidbody rb = projectileInstance.transform.GetChild(0).GetComponent<Rigidbody>();

        // Add force to the projectile
        rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
    }

    private IEnumerator ResetFire(float waitTime)
    {
        projectileBuildUpObject.DOScale(projectileBuildUpSize, waitTime);
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }
}
