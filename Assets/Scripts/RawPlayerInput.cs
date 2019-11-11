using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawPlayerInput : MonoBehaviour
{
    //The unique color related to this player
    public PLAYERID playerId;

    //The character associated with this player
    private Character ControlledPlayer;
    //Has a player been assigned yet?
    private bool playerAssigned;

    // Start is called before the first frame update
    void Start()
    {
        //Make this player persistant
        DontDestroyOnLoad(gameObject);
    }

    //Assign a color to the player
    public void assignId(PLAYERID newID)
    {
        playerId = newID;
    }

    //Assign a character to this player
    public void assignPlayer(Character character)
    {
        ControlledPlayer = character;
        playerAssigned = true;
    }



    //BELOW: Called from player input component 
    public void OnX()
    {
        if (playerAssigned)
            ControlledPlayer.OnX();
    }

    public void OnY()
    {
        if(playerAssigned)
            ControlledPlayer.OnY();
    }

    public void OnB()
    {
        if(playerAssigned)
            ControlledPlayer.OnB();
    }

    public void OnA()
    {
        if(playerAssigned)
            ControlledPlayer.OnA();
    }
}
