using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    public int spawnValue;
    public float maxHealth;
    public float damage;
    public GameObject xpCube;
    public Transform deathProjectileSpawn;
    public GameObject deathParticleVFX;
    //public AudioSource deathAudioSource;
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
        //Play death particle
        GameObject deathVFXClone = Instantiate(deathParticleVFX, deathProjectileSpawn.position, Quaternion.identity);
        // Decrease count from spawner
        if(EnemySpawner.instance != null)
            EnemySpawner.instance.EnemyDestroyed(spawnValue);
        // Destroy gameobject
        Destroy(this.gameObject);
    }
}