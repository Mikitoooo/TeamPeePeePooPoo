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
    public Transform feet;
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
    bool canStompEnemy = true;
    //PLAYER MOVEMENT
    [Header("Dash")]
    public float dashDistance;
    public float dashDuration;
    bool isDashing;
    bool canDash = true;
    public float dashCooldown;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsJumpable;
    public LayerMask whatIsSurface;
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

            StickToGround();

            //ground check
            GroundCheck();

        if (!UIController.instance.pause)
        {
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
    }

    private void FixedUpdate()
    {
        MovePlayer();

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 50);
    }

    void StickToGround()
    {
        float raycastDistance = 1f;
        float rotationSpeed = 10f;

        Quaternion targetRotation;
        Quaternion slimeRotation;
        Vector3 cameraForward = Camera.main.transform.forward;
        Quaternion targetRotationY = Quaternion.LookRotation(cameraForward, Vector3.up);
        // Perform a downward raycast to detect the surface
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, raycastDistance, whatIsSurface))
        {

            // Calculate the target rotation based on the surface normal
            targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

            // Smoothly rotate the GameObject towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            cameraForward.Normalize();
            
            // Smoothly rotate the GameObject towards the target rotation
            slimeRotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z) * targetRotationY;

            slimeObject.rotation = Quaternion.Lerp(slimeObject.rotation, slimeRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {

            slimeObject.rotation= Quaternion.LookRotation(cameraForward);
        }

        // -----------------------
        feet.Rotate(0, 200 * Time.deltaTime, 0);
    }

    void GroundCheck()
    {
        Renderer renderer = slimeObject.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;
        float height = bounds.size.y;

        Collider[] collisions = Physics.OverlapBox(jumpDetectionPoint.position, new Vector3(transform.localScale.x / 1.4f, 0.25f, transform.localScale.z / 1.4f), Quaternion.identity, whatIsJumpable);


        foreach (Collider collider in collisions)
        {
            if (collider.GetComponent<EnemyStats>() && canStompEnemy)
            {

                numberOfJumps = maxNumberOfJumps;
                Jump();
                //deal damage
                collider.gameObject.GetComponent<EnemyStats>().TakeDamage(PlayerShoot.instance.damage);
                //Call death event if the target dies
                if (collider.gameObject.GetComponent<EnemyStats>().health <= 0)
                    collider.gameObject.GetComponent<EnemyStats>().DeathEvent();

                canStompEnemy = false;
                StartCoroutine(StompReset(0.2f));
            }
        }


        if (collisions.Length > 0)
            grounded = true;
        else
            grounded = false;
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
            if (canDash)
            {
                Dash();
                canDash = false;
            }
        }
    }

    void Dash()
    {
        isDashing = true;
        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 dashDirection = moveDirection;
            StartCoroutine(PerformDash(dashDirection));
        }
        else
        {
            Vector3 dashDirection = transform.forward;
            StartCoroutine(PerformDash(dashDirection));
        }

        SoundsManager.instance.PlayerDash();
    }

    IEnumerator PerformDash(Vector3 dashDirection)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + dashDirection * dashDistance;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float t = elapsedTime / dashDuration;
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);

            // Perform a raycast to check for collisions along the dash path
            if (CheckCollisionDash(startPosition, newPosition))
            {
                break;
            }

            transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    bool CheckCollisionDash(Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;

        // Perform a raycast to check for collisions with obstacles
        if (Physics.Raycast(start, direction, distance, whatIsSurface))
        {
            // Collision detected, return true to indicate a collision
            return true;
        }

        // No collision detected, return false
        return false;
    }

    bool CheckCollisionMovement(Vector3 direction)
    {
        RaycastHit hit;
        float distance = 1f; // The distance to check for collisions

        // Perform a raycast in the specified direction
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            // Collision detected, return true to indicate a collision
            return true;
        }
        // No collision detected, return false
        return false;
    }

    void MovePlayer()
    {
        //Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        RaycastHit hit;
        // Check if the body's current velocity will result in a collision
        //if (!CheckCollisionMovement(moveDirection.normalized))
        //{
            if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
            }
            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Force);
            }
        //}
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
        SoundsManager.instance.PlayerJump();

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

    IEnumerator StompReset(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canStompEnemy = true;

    }

    void DebugDrawBox(Vector3 center, Vector3 size, Color color)
    {
        Vector3 halfSize = size * 0.5f;

        // Calculate the corner points of the box
        Vector3[] corners = new Vector3[8]
        {
            center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z),
            center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z),
            center + new Vector3(halfSize.x, -halfSize.y, halfSize.z),
            center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z),
            center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z),
            center + new Vector3(-halfSize.x, halfSize.y, halfSize.z),
            center + new Vector3(halfSize.x, halfSize.y, halfSize.z),
            center + new Vector3(halfSize.x, halfSize.y, -halfSize.z)
        };

        // Draw the lines between the corner points to create the box
        Debug.DrawLine(corners[0], corners[1], color);
        Debug.DrawLine(corners[1], corners[2], color);
        Debug.DrawLine(corners[2], corners[3], color);
        Debug.DrawLine(corners[3], corners[0], color);

        Debug.DrawLine(corners[4], corners[5], color);
        Debug.DrawLine(corners[5], corners[6], color);
        Debug.DrawLine(corners[6], corners[7], color);
        Debug.DrawLine(corners[7], corners[4], color);

        Debug.DrawLine(corners[0], corners[4], color);
        Debug.DrawLine(corners[1], corners[5], color);
        Debug.DrawLine(corners[2], corners[6], color);
        Debug.DrawLine(corners[3], corners[7], color);
    }
}
