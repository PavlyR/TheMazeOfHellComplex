using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]         // The movement code relies on the CharacterController library that is built into Unity

public class PlayerController : MonoBehaviour
{
    public float walking = 5.0f;                    // This variable represents the walking speed of the player
    public float running = 10.0f;                   // This variable represents the running speed of the player
    public float jump = 10.0f;                      // This variable represents the jumping speed of the player
    public float gravity = 20.0f;                   // This vraiblel represents the gravity speed when the player jumps down

    public Camera playerCamera;                     // This is the variable that stores the camera
    public float sensitivity = 5.0f;                // This variable represents the sensitivity of the camera movement
    public float lookXLimit = 90.0f;                // This is the variable that represents the angle for which the player can move camera within

    [SerializeField] Transform spawnPoint;
    CharacterController characterController;        // This is the variable that stores the data from the CharacterController library
    Vector3 moveDirection = Vector3.zero;           // This variable is incharge of moving the position of the player when pressing a button to move
    float rotationX = 0;

     bool canMove = false; //I have changed this to make it so that if the player hasn't gone into the GameStart state the mouse is still visble and the player object won't move
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
        characterController = GetComponent<CharacterController>();


    }

    

    // Update is called once per frame
    void Update()
    {
        if (canMove == true) //This has been put in to stop the player from moving when navigating the menus 
        {
           
            Vector3 forward = transform.TransformDirection(Vector3.forward);        // This variable is incharge of moving the player forward (for instance when pressing W)
            Vector3 right = transform.TransformDirection(Vector3.right);            // This variable is incharge of moving the player sideways (for instance when pressing A and D)

            bool isRunning = Input.GetKey(KeyCode.LeftShift);                                                   // isRunning is the variable that checks when the player press and hold the left shift key to run
            float curSpeedX = canMove ? (isRunning ? running : walking) * Input.GetAxis("Vertical") : 0;        // This line of code is a cleaner version of an if statement because it checks if the player canMove, then it checks if the player is pressing and holding the left shift key, and it changes the state of the player from walking to running. It also includes the direction the player is going in the X-Axis.
            float curSpeedY = canMove ? (isRunning ? running : walking) * Input.GetAxis("Horizontal") : 0;      // This line of code is a cleaner version of an if statement because it checks if the player canMove, then it checks if the player is pressing and holding the left shift key, and it changes the state of the player from walking to running. It also includes the direction the player is going in the Y-Axis.
            float movementDirectionY = moveDirection.y;                                                         
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);                                        //

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jump;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove) //Note to self ask about this if statement -Tam
            {
                rotationX += -Input.GetAxis("Mouse Y") * sensitivity;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            }
        }
        else
        { //For some reason this statement is required to reset the spawn point. If done during the enable movement function the player will move to 0,0,0 then suddenly pop back to where they were moving.
            this.transform.position = spawnPoint.position;
            this.transform.rotation = spawnPoint.rotation;
          
        }
    }
}
