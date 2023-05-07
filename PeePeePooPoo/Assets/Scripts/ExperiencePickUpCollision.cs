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
            print("COLLISION");
            pickUpAudioSource.Play();
            Destroy(collision.gameObject);
        }
    }
}
