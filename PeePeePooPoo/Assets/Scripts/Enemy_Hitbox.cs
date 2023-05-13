using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hitbox : MonoBehaviour
{
    public float damage;
    public float pushbackAmount;


    bool collidedWithPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce(this.transform.forward * pushbackAmount, ForceMode.Impulse);
        }
    }
}
