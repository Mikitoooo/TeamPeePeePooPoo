using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //PROJECTILE STUFF
    public GameObject projectile;
    public float projectileSpeed;
    public Transform spawnLocation;
    public float deviationAngle;

    public float fireRate;
    bool canShoot = true;
    bool heldDown;

    public LayerMask layerMask;
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

            print(HitInfo.point);
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
