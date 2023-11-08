using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    /*
     This script is attached to the empty object of the door prefab. To work it requires a collider on the prefab, this is due to the interactor checking to see if hits 
    an interactable object. Without the collider it will not work. The code rotates the door ~90 degrees when pressed and will not stop when started.
    A possible issue with the code is that the player can not open the door part way then close it again since both interacted and open status' aren't changed until the animation is completed.
    Enemies opening and closing the door as well hasn't been accounted for which could be a problem.
    Also small note to fix later, it seems it doesn't reset to 0° rather it is off by a few degrees ~0.1° which could cause issues in the future.
      
     */

    private bool open = false; //tracks the door status, will allow the player to attempt to open or close the door 
    public Vector3 rotationDirection; //A public to allow for a door to open differently based on the placement of the door.

    //The following two varuables help dictate the speed of the rotation as well as how far it goes
    public float smoothTime; 
    private float convertedTime = 90;

    private float smooth; //See further down for more down

    private bool interacted = false;//Tells the program if the player has given the order to open it yet

    private float openTime = 1.0f; //How long it takes for the door to open, could look into making it a serialized field

    private float timer = 0.0f; //Used to keep track of how long the opening takes

   
    void Update()
    {
        if(interacted == true & open == false) //Movement if the door is closed
        {
            timer += Time.deltaTime;
            if (timer > openTime) //Checks to see if the animation time has passed 
            {
                interacted = false; //If it has stop the animation
                timer = timer - openTime; //Then reset the timer to 0
                open = true;
            }
            else //If it hasn't continue opening the door
            {
                smooth = Time.deltaTime * smoothTime * convertedTime;
                transform.Rotate(rotationDirection * smooth);
            }
        }

        if (interacted == true & open == true) //Movement if the door is open
        {
            timer += Time.deltaTime;
            if (timer > openTime) //Checks to see if the animation time has passed 
            {
                interacted = false; //If it has stop the animation
                timer = timer - openTime; //Then reset the timer to 0
                open = false; //Set the status of the door to reflect the new state
            }
            else //If it hasn't continue opening the door
            {
                smooth = Time.deltaTime * smoothTime * convertedTime;
                transform.Rotate(-rotationDirection * smooth); //By applying a negative to this it moves the door back into the default state
            }
        }
    }

    public void Interact()
    {
        interacted = true; //When the player hits the interact key it starts the opening process. 
       
    }

}

