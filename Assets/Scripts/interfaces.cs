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

public class guiButtons
{
}
