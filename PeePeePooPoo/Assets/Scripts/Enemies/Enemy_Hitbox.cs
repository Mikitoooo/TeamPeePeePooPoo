using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hitbox : MonoBehaviour
{
    public float damage;
    public float pushbackAmount;
    public float hitboxDuration;
    public EnemyStats damageRef;

    bool collidedWithPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        damage = damageRef.damage;

        gameObject.SetActive(false);
    }

    public void ShowHitbox()
    {
        gameObject.SetActive(true); // Activate the hitbox
        StartCoroutine(HideHitbox()); // Start the coroutine to hide the hitbox
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && collidedWithPlayer == false)
        {
            print("Hit player");

            other.transform.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * pushbackAmount, ForceMode.Impulse);
            other.GetComponent<PlayerStats>().currentHealth = other.GetComponent<PlayerStats>().currentHealth - damage;
            UIController.instance.UpdateHealthBar();
            collidedWithPlayer = true;
        }
    }

    IEnumerator HideHitbox()
    {
        yield return new WaitForSeconds(hitboxDuration); // Wait for the hitboxDuration
        gameObject.SetActive(false); // Deactivate the hitbox
        collidedWithPlayer = false;
    }
}
