using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float walkingSpeed;
    [SerializeField] public float runningSpeed;
    [SerializeField] public float GroundDrag;
    [SerializeField] public float jumpSpeed;
    [SerializeField] public float DontJump;
    [SerializeField] public float airTime;
    [SerializeField] public float playerHeight;

    bool jumpCheck = true;
    bool grounded;

    public Transform direction;
    public LayerMask Ground;

    float horizontal;
    float vertical;

    private KeyCode jumpKey = KeyCode.Space;
    private Rigidbody rb;

    Vector3 moveDirection;
    
    [SerializeField] Transform spawnPoint;

     public bool canMove = false; //I have changed this to make it so that if the player hasn't gone into the GameStart state the mouse is still visble and the player object won't move
    /*Why was canMove public before? -Tam
     * I made it public before because I thought I would make other classes that the player can inherit from, and i made canMove public so I can use it in other classes.
     */

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
      if(state ==GameState.GameStart)
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
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        PlayerInput();
        SpeedControl();

        if (grounded)  
        {
            rb.drag = GroundDrag;                                     
        }
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
        PlayerMovement();
    }

    private void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            PlayerDash();
        }
        if (Input.GetKey(jumpKey) && jumpCheck && grounded)
        {
            jumpCheck = false;
            Jump();
            Invoke(nameof(ResetJump), DontJump);
        }
    }

    private void PlayerMovement()
    {
        moveDirection = direction.forward * vertical + direction.right * horizontal;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * walkingSpeed * 10f);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * walkingSpeed * 10f * airTime);
        }
    }

    private void PlayerDash()
    {
        moveDirection = direction.forward * vertical + direction.right * horizontal;
        rb.AddForce(moveDirection.normalized * runningSpeed * 10f);
    }

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

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        jumpCheck = true;
    }
}
