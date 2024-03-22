using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable //The object the player interacts with inherits this interface allowing the object to define the outcome.
{
    public void Interact(GameObject x);
}
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    // Start is called before the first frame update

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) //Classic check to see if the interact key is down
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward); //Checks to see if the object is close enough to the player
            if(Physics.Raycast(r,out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact(this.gameObject); //calls the interact of the object being looked at.
                } 
            }
        }
    }
}
