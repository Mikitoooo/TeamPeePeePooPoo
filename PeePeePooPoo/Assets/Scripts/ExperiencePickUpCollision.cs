using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickUpCollision : MonoBehaviour
{
    public AudioSource pickUpAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ExperienceCube>())
        {
            // play pick up sound
            pickUpAudioSource.Play();
            // update exp amount
            PlayerStats.instance.currentExp = PlayerStats.instance.currentExp + collision.gameObject.GetComponent<ExperienceCube>().expRewarded;
            // Update UI
            UIController.instance.UpdateOnXpCollection();
            // if player has enough xp then level them up
            if(PlayerStats.instance.currentExp >= PlayerStats.instance.expRequired)
            {
                PlayerStats.instance.playerLevel++;
                PlayerStats.instance.LevelUp();
            }
            // destroy pick up on collision
            Destroy(collision.gameObject);
        }
    }
}
