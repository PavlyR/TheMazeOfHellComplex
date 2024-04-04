using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;                                   // This variable is for the player moving speed
    
    [SerializeField] public float walkingSpeed;              // This variable is for the player walking speed
    [SerializeField] public float runningSpeed;              // This variable is for the player running speed
    [SerializeField] public float moveSlowSpeed = 2.5f;
    [SerializeField] public float runSlowSpeed = 3.5f;

    [SerializeField] public float GroundDrag;               // This variable to create drag for the player movement because the physics engine in Unity makes the player movement floaty, this variable helps with that
    [SerializeField] public float playerHeight;             // This variable is for the player height
    [SerializeField] public float gravity;
    [SerializeField] public float jumpForce;                // This variable is for the player jumping speed
    [SerializeField] public float jumpCoolDown;             // This variable is to stop the player from jumping while in mid-air
    [SerializeField] public float airMultiplier;            // This variable is to time the player while in the air after a jump
    [SerializeField] public float interactRange;

    [SerializeField] public PostProcessVolume volume;
    [SerializeField] public Vignette vignette;
    [SerializeField] public float intensity = 0f;

    [SerializeField] public GameObject backgroundMusicObject;
    [SerializeField] public AudioSource backgroundMusic;
    public float pitch = 1.0f;
    public float transition = 1.75f;
    public float percentage = 0f;

    [SerializeField] public GameObject HeartBeatObject;
    [SerializeField] public AudioSource heartBeat;

    public LayerMask Ground;                                // This is a LayerMask variable for the ground that helps the player detect the ground

    bool grounded;                                          // This is a boolean variable to check if the player is on the ground
    bool jumpCheck = true;                                  // This is a boolean variable to check if the player can jump

    public Transform direction;                             // This is a variable stores the direction that the player is facing

    float horizontalInput;                                       // Variable for the horizontal movement of the player
    float verticalInput;                                         // Variable for the vertical movement of the player

    public KeyCode jumpKey = KeyCode.Space;                // Variable for the space button which will be used to jump
    public KeyCode runKey = KeyCode.LeftShift;

    Vector3 moveDirection;                                  // Vector3 moveDirection is incharge of storing values of the direction that player is moving towards

    Rigidbody rb;                                           // Variable to store the rigidbody component of the player

    public MovementState state;                             // This variable tracts the state of the player movement, if the player is walking, running, or jumping

    [SerializeField] Transform spawnPoint;

    public bool canMove = false; //I have changed this to make it so that if the player hasn't gone into the GameStart state the mouse is still visble and the player object won't move
    /*Why was canMove public before? -Tam
     * I made it public before because I thought I would make other classes that the player can inherit from, and i made canMove public so I can use it in other classes.
     */
    public bool enter = false;

    // This enum method is in charge of all the player states
    public enum MovementState
    {
        walk,
        run,
        jump,
    }

    private void Awake()
    {

    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
      if(state == GameState.GameStart)
        {
            EnableMovement(true); //Will need to add a way to check if the player has had this happen before to best use this
        }
        else
        {
            EnableMovement(false); //Whenever a state change happen it check to see if the player is in the "game" and then if not enables menu controls
        }
    }

    private void EnableMovement(bool enable) //This void will turn on and off the player controls
    {
        if(enable == true)
        {
            this.transform.position = spawnPoint.position;
            this.transform.rotation = spawnPoint.rotation;
            Cursor.lockState = CursorLockMode.Locked; //To undo do   Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            canMove = true;

            /*
             * The following code is to reset the player to the same spot every time. It is clunky but I have no clue how else to do it
             */
           
            Debug.Log(this.transform.position);
        }
        if(enable == false) //This has to be done to enable the player to move the mouse over menus without this the player wouldn't be able to click the exit or restart buttons on the lose screen
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canMove = false;
        }
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();                         // Initializing the rigidbody component
        rb.freezeRotation = true;
        volume = GameObject.Find("PostFX").GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
        backgroundMusic = backgroundMusicObject.GetComponent<AudioSource>();
        heartBeat = HeartBeatObject.GetComponent<AudioSource>();
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
        EnableMovement(true);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Grounded makes sure that the player is on the ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        // Method that is responsible for the player input
        PlayerInput();
        // There's an issue with rigidbody movement where when you press and hold down the button to move, the speed keeps going up becauase that's how physics work in unity, so the speed control ensures that the player speed stays fixed
        SpeedControl();
        StateHandler();
        
        // if the player is grounded, then the drag will be set to groundDrag to prevent the player movement from being "floaty"
        if (grounded)  
        {
            rb.drag = GroundDrag;                                     
        }
        // else if the player is not grounded, then the drag will 0 for a much refined jump
        else if (!grounded)
        {
            rb.drag = 0;
        }
        else
        { //For some reason this statement is required to reset the spawn point. If done during the enable movement function the player will move to 0,0,0 then suddenly pop back to where they were moving.
            this.transform.position = spawnPoint.position;
            this.transform.rotation = spawnPoint.rotation; 
        }
    }

    private void FixedUpdate()
    {
        // For some reason, Unity does not like the player movement method to be in the update method, so I placed in the FixedUpdate, but this method is incharge of the player movement
        PlayerMovement();
    }

    private void PlayerInput()
    {
        // The horizontal variable stores the values within the x-axis movement, when going left and right
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // The vertical vairable stores the values within the y-axis moment, when going forward and backward
        verticalInput = Input.GetAxisRaw("Vertical");

        // Checks if the player is holding down the left shift button so the player runs
        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            // Method for the player run, I know I could have made the run code within this input method, thought it would be cleaner to do it as its own method
            PlayerRun();
        }
        // Checks if the player is pressing space so the player jumps, grounded, and jumpCheck is true
        if (Input.GetKeyDown(jumpKey) && jumpCheck && grounded)
        {
            // jumpCheck bool variable goes to false so the player can't jump in mid-air
            jumpCheck = false;
            // Jump method
            Jump();
            // resets the DontJump variable so the player can jump once the player lands on the ground
            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        if (Input.GetKeyDown(KeyCode.E)) //Classic check to see if the interact key is down
        {
            Ray r = new Ray(transform.position, direction.forward); //Checks to see if the object is close enough to the player
            if (Physics.Raycast(r, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact(this.gameObject); //calls the interact of the object being looked at.
                }
            }
        }
    }

    // This method determines the current state of the player
    private void StateHandler()
    {
        // Checks if the player is on the ground and pressing the left shift key. Then the movemement state changes to run which makes the player go faster 
        if (grounded && Input.GetKey(runKey))
        {
            state = MovementState.run;
            moveSpeed = runningSpeed;
        }
        // if the player is on the ground, then the moveSpeed is set to the walking speed
        else if (grounded)
        {
            state = MovementState.walk;
            moveSpeed = walkingSpeed;
        }
        // Otherwise, the player is jumping
        else if (!grounded)
        {
            state = MovementState.jump;
        }   
    }

    private void PlayerMovement()
    {
        // moveDirection variable stores the values from the horizontal value and vertical value. This should let the player move to the direction their looking at
        moveDirection = direction.forward * verticalInput + direction.right * horizontalInput;
        
        // Checks if the player is grounded, then it sets the walking speed to walkingSpeed and because its using rigidbody, it uses forces to move the player
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * walkingSpeed * 10f, ForceMode.Force);
        }
        // Checks if the player is not grounded, which when the player jumps, it uses the jumping speed to jumpSpeed and multiplies it by airTime to determine the time the player is in the air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * jumpForce * 10f * airMultiplier, ForceMode.Force);
        }

        if (enter)
        {
            walkingSpeed = moveSlowSpeed;
            runningSpeed = runSlowSpeed;
            rb.AddForce(moveDirection.normalized * walkingSpeed, ForceMode.Force);
            rb.AddForce(moveDirection.normalized * runningSpeed, ForceMode.Force);
            StartCoroutine(StartEffect());
            backgroundSlowDown();
            heartBeat.Pause();
        }
        if (!enter)
        {
            walkingSpeed = moveSlowSpeed * 2;
            runningSpeed = runSlowSpeed * 2;
            rb.AddForce(moveDirection.normalized * walkingSpeed, ForceMode.Force);
            rb.AddForce(moveDirection.normalized * runningSpeed, ForceMode.Force);
            StartCoroutine(StopEffect());
            backgroundSpeedUp();
            heartBeat.Play();
        }
    }

    // This method is for when the player runs, same logic as the walking.
    private void PlayerRun()
    {
        // moveDirection variable stores the values from the horizontal value and vertical value
        moveDirection = direction.forward * verticalInput + direction.right * horizontalInput;
        // sets the running speed to runningSpeed
        rb.AddForce(moveDirection.normalized * runningSpeed * 10f);
    }

    // This method makes sure that the player speed doesn't go faster as the player holds down the button. Once the player reaches its assigned velocity, the assigned speed, instead of the physics engine takes over the speed which will make the player go very fast, this method just stops the player from going faster when walking and running
    private void SpeedControl()
    {
        Vector3 flatSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatSpeed.magnitude > walkingSpeed)
        {
            Vector3 SpeedLimit = flatSpeed.normalized * walkingSpeed;
            rb.velocity = new Vector3(SpeedLimit.x, rb.velocity.y, SpeedLimit.z);
        }
        else if (flatSpeed.magnitude > runningSpeed)
        {
            Vector3 SpeedLimit = flatSpeed.normalized * runningSpeed;
            rb.velocity = new Vector3(SpeedLimit.x, rb.velocity.y, SpeedLimit.z);
        }
    }

    // This method is responsible over the jumping which makes the player go up the y-axis
    private void Jump()
    {
        //float jumpVelocity = jumpForce;
        //jumpVelocity -= gravity * Time.deltaTime;
        rb.velocity = new Vector3(rb.velocity.x, 5.0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    // Resets when the player jumps and gives the player the ability to jump again when lands on the ground
    private void ResetJump()
    {
        jumpCheck = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShadowZone"))
        {
            enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ShadowZone"))
        {
            enter = false;
        }
    }

    private IEnumerator StartEffect()
    {
        intensity = 2f;

        vignette.enabled.Override(true);
        vignette.intensity.Override(intensity);

        yield return intensity;
    }

    private IEnumerator StopEffect()
    {
        while (intensity > 0)
        {
            intensity -= 0.01f;

            if (intensity < 0)
            {
                intensity = 0;
            }

            vignette.intensity.Override(intensity);
            yield return new WaitForSeconds(0.1f);
        }
        vignette.enabled.Override(false);
        yield break;
    }

    private void backgroundSlowDown()
    {
        backgroundMusic.pitch = Mathf.Lerp(pitch, pitch - 0.5f, percentage);
        percentage += Time.deltaTime / transition;
    }

    private void backgroundSpeedUp()
    {
        backgroundMusic.pitch = Mathf.Lerp(pitch, pitch, percentage);
        percentage += Time.deltaTime / transition;
    }
}
