using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public GameObject xpCube;
    public Transform deathProjectileSpawn;
    public GameObject deathParticleVFX;
    public AudioSource deathAudioSource;
    [HideInInspector]
    public float health;
    public float expRewarded;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void DeathEvent()
    {
        // Spawn XP block
        GameObject xpInstance = Instantiate(xpCube, transform.position, Quaternion.identity);
        Rigidbody rb = xpInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        //add xp reward to xp block
        xpInstance.GetComponent<ExperienceCube>().expRewarded = expRewarded;
        //play death sound effect
        deathAudioSource.Play();
        //Play death particle
        GameObject deathVFXClone = Instantiate(deathParticleVFX, deathProjectileSpawn.position, Quaternion.identity);
        // Destroy gameobject
        Destroy(this.gameObject);
    }
}
