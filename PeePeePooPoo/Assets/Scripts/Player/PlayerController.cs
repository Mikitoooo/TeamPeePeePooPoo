using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //PLAYER MOVEMENT
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    //PLAYER MOVEMENT
    [Header("Jumping")]
    public Transform jumpDetectionPoint;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;
    bool jumping;
    int numberOfJumps;
    public int maxNumberOfJumps;
    public Transform slimeObject;
    public ParticleSystem jumpEmitter;
    //PLAYER MOVEMENT
    [Header("Dash")]
    public float dashSpeed;
    bool isDashing;
    public float dashCooldown;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(jumpDetectionPoint.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        //Handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
            if (numberOfJumps != maxNumberOfJumps && !jumping)
            {
                numberOfJumps = maxNumberOfJumps;
            }
        }
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 50);
    }

    void MyInput()
    {
        //check inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // Jump Inputs
        if(Input.GetKeyDown(KeyCode.Space) && readyToJump && numberOfJumps != 0)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        //Dash Input
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            if(horizontalInput != 0 || verticalInput != 0)
                rb.AddForce(moveDirection * dashSpeed, ForceMode.Impulse);
            else
                rb.AddForce(orientation.forward * dashSpeed, ForceMode.Impulse);

            isDashing = true;
            Invoke("StartDashCooldown", dashCooldown);
        }
    }

    void StartDashCooldown()
    {
        isDashing = false;
    }

    void MovePlayer()
    {
        //Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Force);
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        jumpEmitter.Play();

        jumping = true;
        StartCoroutine(JumpGroundCheckDelay(0.2f));

        numberOfJumps--;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    IEnumerator JumpGroundCheckDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
            jumping = false;

    }
}
