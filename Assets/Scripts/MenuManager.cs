using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    private List<PLAYERID> usedIDs;

    // Start is called before the first frame update
    void Start()
    {
        usedIDs = new List<PLAYERID>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Assign a player input their unique color on creation
    public void assignID(RawPlayerInput newPlayer)
    {
        //If the playerID isn't being used, assign it to a player
        if (!usedIDs.Contains(PLAYERID.BLUE))
        {
            newPlayer.assignId(PLAYERID.BLUE);
            usedIDs.Add(PLAYERID.BLUE);
            return;
        }
        if (!usedIDs.Contains(PLAYERID.RED))
        {
            newPlayer.assignId(PLAYERID.RED);
            usedIDs.Add(PLAYERID.RED);
            return;
        }
        if (!usedIDs.Contains(PLAYERID.GREEN))
        {
            newPlayer.assignId(PLAYERID.GREEN);
            usedIDs.Add(PLAYERID.GREEN);
            return;
        }
        if (!usedIDs.Contains(PLAYERID.YELLOW))
        {
            newPlayer.assignId(PLAYERID.YELLOW);
            usedIDs.Add(PLAYERID.YELLOW);
            return;
        }

        Debug.LogError("More players than there should be");
    }

    //Quit button
    public void endGame()
    {
        Application.Quit();
    }

    public void test(string message)
    {
        Debug.Log(message);
    }

    private void OnPlayerJoined(PlayerInput p)
    {
        Debug.Log("Yes");
        assignID(p.gameObject.GetComponent<RawPlayerInput>());
    }
}
