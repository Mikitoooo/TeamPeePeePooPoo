using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExperiencePickUpCollision : MonoBehaviour
{
    //public AudioSource pickUpAudioSource;
    public GameObject expCube;
    float xpAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //print("Touched player");
            // play pick up sound
            //pickUpAudioSource.Play();

            // update exp amount
            PlayerStats.instance.currentExp = PlayerStats.instance.currentExp + expCube.GetComponent<ExperienceCube>().expRewarded;
            xpAmount = expCube.GetComponent<ExperienceCube>().expRewarded;
            // Update UI
            UIController.instance.UpdateOnXpCollection();
            // if player has enough xp then level them up
            if(PlayerStats.instance.currentExp >= PlayerStats.instance.expRequired)
            {
                PlayerStats.instance.playerLevel++;
                PlayerStats.instance.LevelUp();
            }

            //Tween and destroy the experience block
            this.transform.DOMove(other.transform.position, 0.25f);
            this.transform.DOScale(new Vector3(0,0,0), 0.25f);
            Destroy(expCube.GetComponent<BoxCollider>());
            StartCoroutine(DestroyPickUp());
        }
    }

    IEnumerator DestroyPickUp()
    {
        yield return new WaitForSeconds(0.25f);
        //Spawn Feedback Canvas
        PlayerStats.instance.ExpCollectionFeedback(xpAmount);
        // destroy pick up on collision
        Destroy(expCube);
    }
}
