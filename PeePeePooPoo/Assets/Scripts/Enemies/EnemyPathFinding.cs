using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    [Header("Non Charge Enemy")]
    bool canAttack = true;
    public float attackDistance = 5;
    public float attackCooldown;
    public float lungeDistance = 15;
    public Enemy_Hitbox hitBox;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        player = PlayerStats.instance.gameObject.transform;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Vector3.Distance(this.transform.position, PlayerStats.instance.gameObject.transform.position).ToString());

        if (Vector3.Distance(this.transform.position, player.position) > attackDistance)
        {
            agent.destination = player.position;
        }
        else
        {
            if (canAttack)
            {
                AttackLunge();
            }
            else
                agent.destination = player.position;
        }
    }

    void AttackCooldown()
    {
        canAttack = true;
    }


    void AttackLunge()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(this.gameObject.transform.forward * lungeDistance, ForceMode.Impulse);

        hitBox.ShowHitbox();

        canAttack = false;

        Invoke("AttackCooldown", attackCooldown);
    }


}
