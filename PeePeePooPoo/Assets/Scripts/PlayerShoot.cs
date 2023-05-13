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
        HoldDownShoot();

        if (heldDown && canShoot)
        {
            Transform cameraTransform = Camera.main.transform;
            RaycastHit HitInfo;

            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 1000.0f, layerMask))
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);
            
            FireProjectile(HitInfo.point);
            StartCoroutine(ResetFire(fireRate));
            canShoot = false;
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
