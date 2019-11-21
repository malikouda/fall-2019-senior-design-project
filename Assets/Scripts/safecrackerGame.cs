using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class safecrackerGame : MonoBehaviour, minigame
{

    public int length;
    public Text dial;
    public GameObject gui;
    public AudioClip changeSound;
    public AudioClip successSound;
    public GameObject model;

    private List<int> combo;
    private int currentNumber;
    private int correctInputs;
    private AudioSource sound;


    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    public void complete()
    {
        model.SetActive(false);
        gui.SetActive(false);
        GameManager.instance.completedMinigame();
        GameObject particles = Instantiate(Resources.Load("MinigameParticles"),gameObject.transform.position,Quaternion.identity,transform) as GameObject;
        particles.transform.localScale = transform.lossyScale;
        Invoke("destroyObject", 2);
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }

    public void endGame()
    {
        correctInputs = 0;
        gui.SetActive(false);
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
            //Go down a number
            case (int)BUTTONS.LTRIGGER:
                sound.clip = changeSound;
                sound.Play();
                --currentNumber;
                //reset back to 9
                if (currentNumber < 0)
                    currentNumber = 9;
                break;
            //Go up a number
            case (int)BUTTONS.RTRIGGER:
                sound.clip = changeSound;
                sound.Play();
                currentNumber = (currentNumber + 1) % 10;
                break;
            //submit
            case (int)BUTTONS.A:
                if (currentNumber == combo[correctInputs])
                {
                    sound.clip = successSound;
                    sound.Play();
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
