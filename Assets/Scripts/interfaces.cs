using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BUTTONS { X, Y, A, B, LTTRIGGER, RTRIGGER };
public enum PLAYERID {RED,GREEN,BLUE,YELLOW}
public enum DIFFICULTY {EASY,MEDIUM,HARD}
public interface minigame
{
    void startGame();
    void endGame();
    void playerInput(int input);
    void complete();
}

[System.Serializable]
public class guiButtons
{
    public GameObject obj;
    public GameObject A;
    public GameObject B;
    public GameObject Y;
    public GameObject X;

    //Set the gui to active
    public void activate()
    {
        obj.SetActive(true);
    }

    //Set the gui to non-active
    public void deactivate()
    {
        obj.SetActive(false);
    }

    public void chooseA()
    {
        A.SetActive(true);
    }
    public void chooseB()
    {
        B.SetActive(true);
    }
    public void chooseX()
    {
        X.SetActive(true);
    }
    public void chooseY()
    {
        Y.SetActive(true);
    }

    //Return all the buttons to normal
    public void reset()
    {
        A.SetActive(false);
        B.SetActive(false);
        X.SetActive(false);
        Y.SetActive(false);
    }
}
