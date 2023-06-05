using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;
    public AudioSource playerAudioSource;
    [Header("Player Noises")]
    public AudioClip playerTakeDamage;
    public AudioClip playerJump;
    public AudioClip playerDash;
    public AudioClip playerShoot;
    public AudioClip bulletImpact;
    public AudioClip playerPickUp;
    public AudioClip playerLevelUp;
    [Header("Enemy Noises")]
    public AudioClip enemyShoot;
    public AudioClip enemyCharge;
    public AudioClip enemyTakesDamage;
    public AudioClip enemyDies;
    [Header("World Music")]
    public AudioClip music;
    // Start is called before the first frame update
    void Awake()
    {
        //Ensure there's only once instance of the player stats script
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        this.GetComponent<AudioSource>().clip = music;
        this.GetComponent<AudioSource>().Play();
    }

    // PLAYER SOUND EFFECTS
    public void PlayerTakesDamage() //
    {
        playerAudioSource.PlayOneShot(playerTakeDamage);
        print("Player Takes Damage");
    }

    public void PlayerJump() //
    {
        playerAudioSource.PlayOneShot(playerJump);
        print("Player Jump");
    }
    public void PlayerDash() //
    {
        playerAudioSource.PlayOneShot(playerDash);
        print("Player Dash");
    }

    public void PlayerShoot() //
    {
        playerAudioSource.PlayOneShot(playerShoot);
        print("Player Shoots");
    }

    public void BulletImpact(AudioSource source) //
    {
        source.PlayOneShot(bulletImpact);
        print("bullet impact");
    }

    public void PlayerPickUp() //
    {
        playerAudioSource.PlayOneShot(playerPickUp);
        print("Player Pick Up");
    }
    public void PlayerLevelUp() //
    {
        playerAudioSource.PlayOneShot(playerLevelUp);
        print("Player Level Up");
    }

    // ENEMY SOUND EFFECTS
    public void EnemyShoot(AudioSource source)
    {
        source.PlayOneShot(enemyShoot);
        print("Enemy Shoot");
    }
    public void EnemyCharge(AudioSource source)
    {
        source.PlayOneShot(enemyCharge);
        print("Enemy Charge");
    }
    public void EnemyTakesDamage(AudioSource source)
    {
        source.PlayOneShot(enemyTakesDamage);
        print("Enemy take damage");
    }
    public void EnemyDies(AudioSource source)
    {
        source.PlayOneShot(enemyDies);
        print("Enemy Dies");
    }

}
