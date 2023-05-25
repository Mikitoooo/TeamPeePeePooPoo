using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLookAtPlayer : MonoBehaviour
{
    Transform playerRef;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = PlayerStats.instance.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(playerRef);
    }
}
