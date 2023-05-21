using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructLifeTime : MonoBehaviour
{
    public float lifeTime;
    public GameObject objectToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(objectToDestroy, lifeTime);
    }

}
