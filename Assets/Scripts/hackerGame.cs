using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hackerGame : MonoBehaviour, minigame
{
    
    public int length;
    private List<int> pattern;
    private int correctInput;
    private Text playText;
    // Start is called before the first frame update
    void Start()
    {
        playText = gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        pattern = new List<int>();
        correctInput = 0;
        for (int i = 0; i <= length; i++)
        {
            pattern.Add(Random.Range(0, 3));
        }

        switch(pattern[0])
        {
            case 0:
                {
                    playText.text = "x";
                    break;
                }
            case 1:
                {
                    playText.text = "y";
                    break;
                }
            case 2:
                {
                    playText.text = "a";
                    break;
                }
            case 3:
                {
                    playText.text = "b";
                    break;
                }
        }

    }

    public void playerInput(int input)
    {
        if (input == pattern[correctInput])
        {
            
            correctInput++;
            if (correctInput > length)
            {
                Debug.Log("WIN");
                correctInput = 0;
                Destroy(gameObject);
            }

            switch (pattern[correctInput])
            {
                case 0:
                    {
                        playText.text = "x";
                        break;
                    }
                case 1:
                    {
                        playText.text = "y";
                        break;
                    }
                case 2:
                    {
                        playText.text = "a";
                        break;
                    }
                case 3:
                    {
                        playText.text = "b";
                        break;
                    }
            }
        }
        else
        {
            correctInput = 0;
        }
    }
}
