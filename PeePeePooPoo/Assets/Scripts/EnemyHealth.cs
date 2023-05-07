using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth;
    public GameObject xpCube;
    public AudioSource deathAudioSource;
    [HideInInspector]
    public float health;

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
        //play death sound effect
        deathAudioSource.Play();
        // Destroy gameobject
        Destroy(this.gameObject);
    }
}
