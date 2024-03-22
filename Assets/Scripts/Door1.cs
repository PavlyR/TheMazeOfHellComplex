using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour, IInteractable //Sorry had to add the interactable tag to allow Alan to open and close the door.
{
    // This AnimationCurve variable is responsible for the opening and closing animation of the door
    public AnimationCurve openDoor = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) });
    // This float variable is responsible for the speed of the door while opening and closing
    public float openDoorSpeed = 2.0f;
    // This float variable is responsible for the angle of the door opening
    public float openDoorAngle = 90.0f;

    // this bool variable determines if the door is open or closed
    protected bool open = false;
    // this bool variable determines if the player is close to the door 
    bool enter = false;

    // This float variable stores the original angle of the door
    float defaultRotationAngle;
    // This float varibale stores the current angle of the door
   protected float currentRotationAngle;
    // This float variable stores the time that it takes for the door to open
    protected float openTime = 0;

    [SerializeField] GameObject pivotPoint;

    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        // initializing the default and current angle of the door
        defaultRotationAngle = pivotPoint.transform.localEulerAngles.y;
        currentRotationAngle = pivotPoint.transform.localEulerAngles.y;
        audioData = GetComponent<AudioSource>();

        // initializing the collider which is a sphere collider that will detect the player when close to the door to interact with the door
        //GetComponent<Collider>().isTrigger = true;
    }

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        if (state == GameState.GameStart)
        {
            open = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        // This if statement is responsible for determining the amount of time it takes for the door open and the speed of the door to open
        if (openTime < 1)
        {
            openTime += Time.deltaTime * openDoorSpeed * openDoor.Evaluate(openTime);
        }
        
        // This code is responsible for the continuous angle changes of the door opening
        pivotPoint.transform.localEulerAngles = new Vector3(pivotPoint.transform.localEulerAngles.x, Mathf.LerpAngle(currentRotationAngle, defaultRotationAngle + (open ? openDoorAngle : 0), openTime), pivotPoint.transform.localEulerAngles.z);
        
        // This if statement checks if the player is close to the door and if the player pressed the E button then it opens the door
     
        /* Legacy Code
        if (Input.GetKeyDown(KeyCode.E) && enter)
        {
            OnOpen();
        }
        */
    }

    protected virtual void OnOpen(GameObject x) //This void was made to allow for the win door to work with the classical door code.
    {
        open = !open;
        currentRotationAngle = pivotPoint.transform.localEulerAngles.y;
        openTime = 0;
       
    }


    /* Legacy code from when colliders where used
    // This method checks if the player has entered the collider and checks if the player prefab has the player tag
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }

    // This method checks if the player has exited the collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }
    */
    public void Interact(GameObject x)
    {
        //if (open == false) {
            OnOpen(x);
        audioData.Play();
        //     }
    }
}
