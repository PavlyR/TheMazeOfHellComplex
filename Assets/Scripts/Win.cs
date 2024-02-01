using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Win : Door1 //To attach the script to the door I made the Win script inherit the door script
{

    protected override void OnOpen() //override changes what happens when OnOpen is called
    {
        open = !open;
        currentRotationAngle = transform.localEulerAngles.y;
        openTime = 0;
        Invoke("YouWin", 1f); //A small delay so the player can see the door opening before being taken to the win screen
        }

    void YouWin()
    {
        GameManager.Instance.UpdateGameState(GameState.WinMenu);
    }
    
}
