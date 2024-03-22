using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public void Interact(GameObject x)
    {
        Debug.Log("Interacted");
    }
}
