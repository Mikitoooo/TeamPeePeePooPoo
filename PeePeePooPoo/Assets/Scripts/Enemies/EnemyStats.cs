using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int spawnValue;
    public float maxHealth;
    public float damage;
    public GameObject xpCube;
    public Transform deathProjectileSpawn;
    public GameObject deathParticleVFX;
    public Image healthBar;
    public Image healthBarBackdrop;
    float healthBarDuration = 5f; // Duration for which the health bar will remain active
    private bool isHealthBarActive = false; // Flag to track if the health bar is active
    private float healthBarTimer = 0f;
    //public AudioSource deathAudioSource;
    [HideInInspector]
    public float health;
    public float expRewarded;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        //healthBar.gameObject.SetActive(false);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        HealthBarAppear();
        UpdateHealthBar(health, maxHealth);
        // Play sound effect
        //SoundsManager.instance.EnemyTakesDamage(this.GetComponent<AudioSource>());
    }
    public void DeathEvent()
    {
        // Spawn XP block
        GameObject xpInstance = Instantiate(xpCube, transform.position, Quaternion.identity);
        Rigidbody rb = xpInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        //add xp reward to xp block
        xpInstance.GetComponent<ExperienceCube>().expRewarded = expRewarded;
        //Add to player score
        UIController.instance.UpdatePlayerScore(expRewarded);
        //Play death particle
        GameObject deathVFXClone = Instantiate(deathParticleVFX, deathProjectileSpawn.position, Quaternion.identity);
        // Play sound effect
        SoundsManager.instance.EnemyDies(deathVFXClone.GetComponent<AudioSource>());
        // Decrease count from spawner
        if (EnemySpawner.instance != null)
            EnemySpawner.instance.EnemyDestroyed(spawnValue);
        // Destroy gameobject
        Destroy(this.gameObject);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.DOFillAmount(currentHealth / maxHealth, 0.5f); // Set the health bar's value based on the current health
    }

    public void HealthBarAppear()
    {
        //Get the alpha values
        var tempColor = healthBar.color;
        tempColor.a = 1f;
        var tempColor2 = healthBarBackdrop.color;
        tempColor2.a = 1f;

        healthBar.DOColor(tempColor, 0.5f);
        healthBarBackdrop.DOColor(tempColor2, 0.5f);

        isHealthBarActive = true; // Set the flag to true
        healthBarTimer = 0f; // Reset the timer
    }
    void Update()
    {
        if (isHealthBarActive)
        {
            healthBarTimer += Time.deltaTime; // Increment the timer

            if (healthBarTimer >= healthBarDuration)
            {
                //Get the alpha values
                var tempColor = healthBar.color;
                tempColor.a = 0;
                var tempColor2 = healthBarBackdrop.color;
                tempColor2.a = 0;

                healthBar.DOColor(tempColor, 0.5f);
                healthBarBackdrop.DOColor(tempColor2, 0.5f);

                isHealthBarActive = false; // Set the flag to false
            }
        }
    }
}
