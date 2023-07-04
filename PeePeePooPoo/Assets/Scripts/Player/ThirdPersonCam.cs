using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public float rotationSpeed;

    [Header("Camera Type")]
    public Transform combatLookAt;
    public CameraStyle currentStyle;
    public enum CameraStyle {basic, Combat, Topdown}

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //rotate orientation
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;
        //rotate player object
        if (currentStyle == CameraStyle.basic)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 inputDirection = orientation.forward * vertical + orientation.right * horizontal;

            if (inputDirection != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
        }
        else if (currentStyle == CameraStyle.Combat)
        {
            //Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            //playerObj.forward = dirToCombatLookAt.normalized;
        }

    }
}
