using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum DIFFICULTY {EASY,MEDIUM,HARD}
public interface minigame
{
    void startGame();
    void playerInput(int input);
}
