using UnityEngine;

public class DSPlayerMovement : MonoBehaviour
{

    public enum PlayerState
    {
        Moving, // Player is moving
        Croutching, // Player is croutching
        Jumping // Player is jumping 
    }

    [Header("References")]
    public Transform orientation; // Reference to the orientation transform
    public Transform player; // Reference to the player transform
    public Transform playerObj; // Reference to the player object transform
    public Rigidbody playerRB; // Reference to the player Rigidbody component
    public CapsuleCollider playerCollider; // Reference to the player CapsuleCollider component

    [Header("Ground Detection")]
    public LayerMask groundMask; // Layer mask for the ground to detect collisions
    public bool isGrounded;
    // Boolean to check if the player is grounded
    public float checkDistance = 0.2f; // Distance to check for ground detection
    public float playerHeight = 2f; // Height of the player for ground detection
    public float groundDrag = 5f; // Drag applied when the player is grounded

    [Header("Movement")]
    public float walkSpeed = 5f; // Speed of walking
    public float runSpeed = 10f; // Speed of running
    public float crouchSpeed = 3f; // Speed of crouching 
    public float moveSpeed;
    public float jumpHeight = 5f; // max Height applied when the player jumps 
    public float jumpCooldown = 0.25f; // Cooldown time for jumping 
    public float gravityFactor = 2f; // Gravity factor to apply to the player
    public float crouchingScale = 0.5f; // Scale factor for crouching 
    public float crouchingTime = .5f; // Time it takes to crouch or uncrouch
    [Header("Slope Handling")]
    public float maxSlopeAngle = 45f; // Maximum angle of slope the player can walk on


    [Header("State Machine")]
    public float    currentSpeed; // Current speed of the player
    public Vector3  currentVelocity; // Current velocity of the player
    public float currentJumpPerc; // Current jump percentage (not used in this code but can be useful for animations or effects)
    public PlayerState currentState; // Current state of the player 
    public bool onPause = false; // Boolean to check if the game is paused
    public float crouchingPerc = 0f;
    // Variables to store input values
    private float horizontalInput; // Variable to store horizontal input
    private float verticalInput; // Variable to store vertical input
    private Vector3 moveDirection; // Variable to store the direction of movement
    public bool isJumping = false; // Boolean to check if the player is currently jumping
    public bool isCrouching = false; // Boolean to check if the player is currently crouching
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        croutchInitHeight = playerCollider.height; // Store the initial height of the player collider
        moveDirection  = orientation.forward; // Initialize moveDirection as a new Transform
        moveSpeed = walkSpeed; // Initialize moveSpeed to walkSpeed
        playerRB.freezeRotation = true; // Prevent the Rigidbody from rotating due to physics interactions
        playerRB.useGravity = false; // Disable the Rigidbody's default gravity to apply custom gravity logic
        playerRB.interpolation = RigidbodyInterpolation.Interpolate;
        crouchCenterInit = playerCollider.center; // Store the initial center position of the player collider
    }

   
    // Update is called once per frame
    void Update()
    {
        if (onPause) return; // If the game is paused, exit the Update method
        HandleInput(); // Call the method to handle input
        HandleStateMachine(); // Call the method to handle the state machine logic
        //Debug.Log(playerRB.linearVelocity);
        //playerObj.forward = orientation.forward; // Set the player object's forward direction to the orientation's forward direction
    }

    Vector3 gravityVel = Vector3.zero;
    private void FixedUpdate()
    {
        if (onPause) return; // If the game is paused, exit the FixedUpdate method
        moveDirection =
            orientation.forward * verticalInput + orientation.right * horizontalInput; // Calculate the movement direction based on input and orientation

        
        HandleMovement(); // Call the method to handle movement
        HandlePhysics(); // Call the method to handle physics interactions
        HandlePhysicsConstrains(); // Call the method to handle physics constraints
        
        // playerRB.AddForce(Physics.gravity * gravityFactor  , ForceMode.Acceleration); // Apply custom gravity to the player Rigidbody
    }

    void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Get horizontal input from the user
        verticalInput = Input.GetAxisRaw("Vertical"); // Get vertical input from the user
        if (Input.GetKey(KeyCode.LeftShift) && !isJumping && !isCrouching && isGrounded ) // Check if the left shift key is pressed
        {
            moveSpeed = runSpeed; // Set current speed to run speed
        }
        else
        {
            moveSpeed = walkSpeed; // Set current speed to walk speed
        }

        crouchingCheck(); // Call the method to check for crouching input
        // playerCollider.height = currentYScale; // Set the height of the player collider to the current Y scale
        JumpCheck(); // Call the method to check for jumping input
    }

    void HandleMovement()
    {
        movePlayer(); // Call the method to handle player movement
        Jump(); // Call the method to handle jumping logic
        OnCroutching(); // Call the method to handle crouching logic

    }

    void HandlePhysics()
    {
       
        isGrounded = Physics.Raycast(playerCollider.bounds.center, Vector3.down, out RaycastHit hit, playerCollider.height * 0.5f + checkDistance, groundMask);

        if (isGrounded && !isJumping ) // If the player is grounded
        {
            playerRB.linearDamping = groundDrag; // Set drag to reduce sliding on the ground
        }
        else
        {
            playerRB.linearDamping = 0f; // Set drag to zero when not grounded
        }
        if (!isGrounded)
            playerRB.AddForce(  Physics.gravity * gravityFactor, ForceMode.Acceleration);
        //gravityVel.y = Physics.gravity.y * gravityFactor * Time.fixedDeltaTime; //
        // playerRB.linearVelocity += gravityVel; // Add the custom gravity velocity to the player Rigidbody's linear velocity
        
    }

    void HandlePhysicsConstrains()
    {
        if (checkSlope() && !isJumping) // If the player is on a slope
        {
            if (playerRB.linearVelocity.magnitude > moveSpeed)
                playerRB.linearVelocity = playerRB.linearVelocity.normalized * moveSpeed; // Limit the velocity to the current speed

        }
        else
        {
            Vector3 flatVel = new Vector3(playerRB.linearVelocity.x, 0f, playerRB.linearVelocity.z); // Get the horizontal velocity of the player
            if (flatVel.magnitude > moveSpeed) // If the horizontal velocity exceeds the current speed
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed; // Limit the velocity to the current speed
                playerRB.linearVelocity = new Vector3(limitedVel.x, playerRB.linearVelocity.y, limitedVel.z); // Apply the limited velocity while preserving vertical velocity
            }
        }
        
    }
    

    void crouchingCheck()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isCrouching && isGrounded) // Check if the C key is pressed
        {
            OnCroutchingStart(); // Call the method to start crouching
                                 // playerCollider.center = new Vector3(0f, currentYScale / 2f, 0f);
        }
        else if (Input.GetKeyUp(KeyCode.C) || !isGrounded && isCrouching) // Check if the C key is released
        {
            OnCroutchingStop(); // Call the method to stop crouching
                                // playerCollider.center = new Vector3(0f, currentYScale / 2f, 0f);
        }
    }

    float croutchScaleStart = 0.5f;
    float croutchScaleEnd = 1.0f; // Scale factor for crouching
    float crouchScale = 0.5f;
    float croutchInitHeight = 0f;
    float crouchStartTime = 0f; // Time when crouching started
    Vector3 crouchCenterInit = Vector3.zero; // Initial center position of the player collider
    void mySwap(ref float  a,ref float  b)
    {
        float temp = a;
        a = b;
        b = temp;
    }
    void OnCroutchingStart()
    {
        isCrouching = true; // Set the crouching state to true
        croutchScaleEnd = 0.5f;
        croutchScaleStart = 1.0f; // Set the start scale factor for crouching
        
        crouchStartTime = Time.time; // Record the time when crouching started
    }

    void OnCroutchingStop()
    {
        isCrouching = false; // Set the crouching state to true
        croutchScaleEnd = 1.0f;
        croutchScaleStart = 0.5f; // Set the start scale factor for crouching
        crouchStartTime = Time.time; // Record the time when crouching started
    }

    void OnCroutching()
    {
        float t = Mathf.Clamp01((Time.time - crouchStartTime) / crouchingTime); // 0.2f = �������� transition
        crouchScale = Mathf.Lerp(croutchScaleStart, croutchScaleEnd, t);
        playerCollider.height = crouchScale * croutchInitHeight;
        float yOffset = isCrouching ? 
            Mathf.Lerp(crouchCenterInit.y, crouchCenterInit.y - crouchCenterInit.y * crouchScale, t)
            :
             Mathf.Lerp(crouchCenterInit.y - crouchCenterInit.y * crouchScale, crouchCenterInit.y, t);
        Vector3 cnt = new Vector3(playerCollider.center.x, yOffset, playerCollider.center.z);
        playerCollider.center = cnt; // Adjust the center position of the player collider based on the crouch scale
        
    }

    void movePlayer()
    {
        Vector3 dir = moveDirection;
        if (checkSlope() && isGrounded && !isJumping)
        {
            dir = GetSlopeDir(); // If on a slope, adjust the movement direction to follow the slope
            if (playerRB.linearVelocity.y > 0f)
            {
                //playerRB.AddForce(Vector3.down * 80f, ForceMode.Force); // Apply downward force to prevent sliding up slopes
            }
        }
        playerRB.AddForce(dir * moveSpeed * 10f, ForceMode.Force); // Apply force to the player Rigidbody in the movement direction

    }




    float jumpStartHeight = 0f;
    bool canJump = true; // Boolean to check if the player can jump
    // jumping functions
    void JumpCheck()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isJumping) // Check if the player is grounded and the space key is pressed
        {
            setJump(); // Start the jump process
            playerRB.linearDamping = 0f; // Set drag to zero to allow jumping without sliding
        }
    }

    void setJump()
    {
        canJump = true; // Allow jumping
        isJumping = false; // Reset the jumping state
        jumpStartHeight = playerCollider.bounds.center.y;
    }

    void Jump()
    {
        if (!canJump || isJumping) return; // If the player cannot jump or is already jumping, exit the method
        // force that will applied once to make the player jump jumpStartHeight + jumpHeight
        canJump = false; // Prevent further jumps until the cooldown is over
        isJumping = true; // Set the jumping state to true
        float jumpVelocity = Mathf.Sqrt(-2f * Physics.gravity.y * gravityFactor * jumpHeight);
        Vector3 vel = playerRB.linearVelocity;
        vel.y = jumpVelocity;
        Vector3 jumpForce = Vector3.up * jumpVelocity * playerRB.mass;
        playerRB.linearDamping = 0f; // Set drag to zero to allow jumping without sliding
        playerRB.AddForce(jumpForce, ForceMode.Impulse);

        
        //playerRB.linearVelocity = vel;
        Invoke(nameof(resetJump), jumpCooldown); // Reset the jumping state after the cooldown period
        //if (isGrounded && playerRB.linearVelocity.y <= 0f)
         //   resetJump();
    }


    void resetJump()
    {
        if (isGrounded && playerRB.linearVelocity.y <= 0f)
        {
            canJump = false; // Prevent further jumps until the cooldown is over
            isJumping = false; // Reset the jumping state
        }
        else
        {
            Invoke(nameof(resetJump), .1f); // If the player is not grounded, keep invoking resetJump until they are grounded
        }
    }

    public bool isOnSlope = false;
    public float slopeTollerance = .1f; // Tolerance for slope detection
    private RaycastHit slopeHit; // Raycast hit information for slope detection
    //Slope handling 
    bool checkSlope()
    {
        if (Physics.Raycast(playerCollider.bounds.center, Vector3.down, out slopeHit, playerCollider.height * 0.5f + checkDistance, groundMask))
        {
            float angle = Vector3.Angle( Vector3.up , slopeHit.normal); // Calculate the angle between the slope normal and the up direction
            isOnSlope = (angle <= maxSlopeAngle) && (angle > slopeTollerance) ; // If the angle is less than the maximum slope angle
            
        } else 
            isOnSlope = false; // If no slope is detected, return false

        return isOnSlope;
    }

    Vector3 GetSlopeDir()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
        // ��������� �� ��� �����
        Vector3 slopeDir = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;

        // ��������� ���� �� ���� ��� ������� �� ��������
        float slopeAngleRad = Vector3.Angle(Vector3.up, slopeHit.normal) * Mathf.Deg2Rad;
        Vector3 slopeUpComp = Vector3.up * Mathf.Sin(slopeAngleRad);

        // ������ ��������� ��� ������������ ��� ����� ���� �� ����
        return (slopeDir + slopeUpComp).normalized;
    }



    public static float InverseLerp(float a, float b, float value)
    {
        if (Mathf.Abs(b - a) < Mathf.Epsilon)
            return 0f; // ������� ��������� �� �� �����

        return Mathf.Clamp01((value - a) / (b - a));
    }

    void HandleStateMachine()
    {
            // This method can be used to handle different states of the player, such as walking, running, jumping, etc.
            // Currently, it is empty and can be implemented as needed.
            currentSpeed = playerRB.linearVelocity.magnitude; // Update the current speed based on the player's Rigidbody velocity
            currentVelocity = playerRB.linearVelocity; // Update the current velocity based on the player's Rigidbody velocity
        crouchingPerc = isCrouching ? Mathf.Clamp01((Time.time - crouchStartTime) / crouchingTime) : (1 - Mathf.Clamp01((Time.time - crouchStartTime) / crouchingTime)) ;
        // Calculate the current jump percentage based on the player's position
        if (isJumping)
        {
            currentJumpPerc = (playerCollider.bounds.center.y - jumpStartHeight) / (jumpHeight);
            currentState = PlayerState.Jumping; // Set the current state to jumping if the player is jumping
        }
        else if (isCrouching)
        {
            currentJumpPerc = 0;
            
            currentState = PlayerState.Croutching; // Set the current state to crouching if the player is crouching
        }
        else
        {
            currentJumpPerc = 0;
            currentState = PlayerState.Moving; // Set the current state to moving if the player is not crouching
        }

        
    }































































    private void OnDrawGizmos()
    {
        if (player == null) return;

        // ������� ����� ��� ������ �������
        Vector3 origin = playerCollider.bounds.center;
        float rayLength = playerCollider.height * 0.5f + checkDistance;
        Vector3 end = origin + Vector3.down * rayLength;

        // ������� ����� ������� �� �� �� ������� ������
        Gizmos.color = isGrounded ? Color.green : Color.red;

        // �������� ������� ��� ������ ������� ��� �����
        Gizmos.DrawLine(origin, end);
        Gizmos.DrawSphere(end, 0.05f);
        Gizmos.DrawSphere(origin, 0.05f);


    }
}
