using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    public NavMeshAgent agent;
    bool canAttack = true;
    public float attackDistance = 5;
    public float attackCooldown;
    public float lungeDistance = 15;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Vector3.Distance(this.transform.position, PlayerStats.instance.gameObject.transform.position).ToString());

        if (Vector3.Distance(this.transform.position, PlayerStats.instance.gameObject.transform.position) > attackDistance)
        {
            agent.destination = PlayerStats.instance.gameObject.transform.position;
        }
        else
        {
            if (canAttack)
                AttackLunge();
            else
                agent.destination = PlayerStats.instance.gameObject.transform.position;
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

        canAttack = false;

        Invoke("AttackCooldown", attackCooldown);
    }
}
