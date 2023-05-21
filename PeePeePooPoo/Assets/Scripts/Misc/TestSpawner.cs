using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject xpCube;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        GameObject xpInstance = Instantiate(xpCube, transform.position, Quaternion.identity);
        Rigidbody rb = xpInstance.GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 50, ForceMode.Impulse);
        StartCoroutine(Spawn());
    }
}
