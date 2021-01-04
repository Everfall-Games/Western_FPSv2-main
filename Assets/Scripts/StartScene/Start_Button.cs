using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Button : MonoBehaviour
{
    public void StartGame()
    {
        //If 8 connections then allow to start game
        if(NetworkServer.connections.Count==7)
        {
            //To-Do - Assign the classes and start the game
        }
    }
}
