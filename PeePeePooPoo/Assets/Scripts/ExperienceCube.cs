using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceCube : MonoBehaviour
{
    public float bobSpeed = 0.5f;
    public Transform cubeTranform;
    private void Update()
    {
        //rotate cube
        cubeTranform.Rotate(0f, 15f * Time.deltaTime, 0f);
        //cube bobs up and down
        float newY = Mathf.Sin(Time.time * bobSpeed) * 0.5f + transform.position.y;
        float newX = transform.position.x;
        float newZ = transform.position.z;
        cubeTranform.position = new Vector3(newX, newY, newZ);
    }

}

