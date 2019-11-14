using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hackerGame : MonoBehaviour, minigame
{

    public int length;
    public GameObject uiObj;
    private List<int> pattern;
    private int correctInput;
    private Text playText;
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playText = GetComponentInChildren<Text>();
    }

    public void displayNext(int next)
    {
        switch (next)
        {
            case (int)BUTTONS.X:
                {
                    playText.text = "x";
                    break;
                }
            case (int)BUTTONS.Y:
                {
                    playText.text = "y";
                    break;
                }
            case (int)BUTTONS.A:
                {
                    playText.text = "a";
                    break;
                }
            case (int)BUTTONS.B:
                {
                    playText.text = "b";
                    break;
                }
        }
    }


    public void startGame()
    {
        uiObj.SetActive(true);
        pattern = new List<int>();
        correctInput = 0;
        for (int i = 0; i <= length; i++)
        {
            pattern.Add(Random.Range(0, 4));
        }

        displayNext(pattern[0]);

    }

    public void playerInput(int input)
    {
        if (input == pattern[correctInput])
        {
            
            correctInput++;
            if (correctInput > length)
            {
                complete();

            }

            displayNext(pattern[correctInput]);
            anim.SetTrigger("pass");
        }
        else
        {
            correctInput = 0;
            displayNext(pattern[correctInput]);
            anim.SetTrigger("fail");
        }
    }

    public void complete()
    {
        correctInput = 0;
        GameManager.instance.completedMinigame();
        Destroy(gameObject);

    }

    public void endGame()
    {
        uiObj.SetActive(false);
    }
}
