using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class safecrackerGame : MonoBehaviour, minigame
{

    public int length;
    public Text dial;
    public GameObject gui;

    private List<int> combo;
    private int currentNumber;
    private int correctInputs;

    public void complete()
    {
        GameManager.instance.completedMinigame();
        Destroy(gameObject);

    }

    public void endGame()
    {
        correctInputs = 0;
        gui.SetActive(true);
    }

    public void shownumber()
    {
        //change text
        dial.text = "[" + currentNumber + "]";

        //if it's the right number, turn the ui green
        if (currentNumber == combo[correctInputs])
        {
            dial.color = Color.green;
        }
        else
        {
            dial.color = Color.white;
        }
    }

    //Take player input
    public void playerInput(int input)
    {  
        switch (input)
        {
            case (int)BUTTONS.LTRIGGER:
                --currentNumber;
                //reset back to 9
                if (currentNumber < 0)
                    currentNumber = 9;
                break;
            case (int)BUTTONS.RTRIGGER:
                currentNumber = (currentNumber + 1) % 10;
                break;
            case (int)BUTTONS.A:
                if (currentNumber == combo[correctInputs])
                {
                    ++correctInputs;
                    if (correctInputs >= length)
                    {
                        complete();
                    }
                }
                break;

        }
        shownumber();
    }

    public void startGame()
    {
        //Turn on UI
        gui.SetActive(true);
        //generate combo
        combo = new List<int>();
        for (int i = 0; i < length;i++)
        {
            combo.Add(Random.Range(0,10));
        }
        correctInputs = 0;
    }
}
