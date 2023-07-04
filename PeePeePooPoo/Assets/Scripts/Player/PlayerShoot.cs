using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;

    //PROJECTILE STUFF
    public GameObject projectile;
    public float damage;
    public float projectileSpeed;
    public Transform spawnLocation;
    public float deviationAngle;
    //Shooting
    public float fireRate;
    bool canShoot = true;
    bool heldDown;
    //SlimeBuddies
    public Transform[] slimeBuddySpawns;
    public int slimeBuddyLevel = 0;
    public GameObject slimeBuddyObject;
    private List<GameObject> slimeInstances = new List<GameObject>();
    //Raycast Filtering
    public LayerMask layerMask;

    private void Start()
    {
        //Ensure there's only once instance of the player shoot script
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIController.instance.pause)
        {
            HoldDownShoot();

            if (heldDown && canShoot)
            {
                Transform cameraTransform = Camera.main.transform;
                RaycastHit HitInfo;

                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 1000.0f, layerMask))
                    //Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 1000, Color.yellow,100);

                    FireProjectile(HitInfo.point);
                //print(HitInfo.collider.gameObject);
                StartCoroutine(ResetFire(fireRate));
                canShoot = false;
            }
            //Check if player is aiming at target
            IsAimingAtEnemy();
        }
    }

    void IsAimingAtEnemy()
    {
        // Insert your aiming logic here
        // You can use a raycast or any other method to determine if the enemy is within the player's aim
        RaycastHit HitInfoAiming;
        Transform cameraTransformAiming = Camera.main.transform;
        if (Physics.Raycast(cameraTransformAiming.position, cameraTransformAiming.forward, out HitInfoAiming, 1000.0f, layerMask))
        {
            if (HitInfoAiming.collider.gameObject.GetComponent<EnemyStats>())
            {
                HitInfoAiming.collider.gameObject.GetComponent<EnemyStats>().HealthBarAppear();
            }
        }
    }

    void HoldDownShoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            heldDown = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            heldDown = false;
        }
    }

    void FireProjectile(Vector3 targetPoint)
    {
        //get the direction
        Vector3 direction = (targetPoint - spawnLocation.transform.position).normalized;

        //give the projectile devation
        direction = Quaternion.Euler(Random.Range(-deviationAngle, deviationAngle),Random.Range(-deviationAngle, deviationAngle), 0) * direction;

        //instantiate projectile
        GameObject projectileInstance = Instantiate(projectile, spawnLocation.transform.position, spawnLocation.transform.rotation);

        // Add damage to projectile
        projectileInstance.transform.GetChild(0).GetComponent<ProjectileCollision>().damage = damage;

        // Get the Rigidbody component of the projectile instance
        Rigidbody rb = projectileInstance.transform.GetChild(0).GetComponent<Rigidbody>();

        // Add force to the projectile
        rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);

        // Reticle Kick
        ReticleController.instance.ReticleKick();

        SoundsManager.instance.PlayerShoot();
    }

    public void SpawnSlimeBuddy()
    {
        slimeBuddyLevel++;

        if (slimeBuddyLevel < 12)
        {
            //instantiate projectile
            GameObject projectileInstance = Instantiate(slimeBuddyObject, slimeBuddySpawns[slimeBuddyLevel].transform.position, Quaternion.identity, slimeBuddySpawns[slimeBuddyLevel]);
            slimeInstances.Add(projectileInstance);
        }
        else if (slimeBuddyLevel >= 12)
        {
            foreach (GameObject obj in slimeInstances)
            {
                obj.GetComponent<SlimeBuddyBrain>().ImproveFireRate();
            }
        }
    }

    private IEnumerator ResetFire(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }
}
