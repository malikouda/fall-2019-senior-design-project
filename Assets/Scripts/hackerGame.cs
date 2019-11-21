using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hackerGame : MonoBehaviour, minigame
{

    public int Maxlength;
    public int Minlength;
    public guiButtons guiButtons;
    public AudioClip incorrect;
    public AudioClip[] correctNoises;
    public GameObject model;

    private int length;
    private List<int> pattern;
    private int correctInput;
    private Text playText;
    private Animator anim;
    private AudioSource sound;


    // Start is called before the first frame update
    void Start()
    {
        length = Random.Range(Minlength, length + 1);
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        playText = GetComponentInChildren<Text>();
    }

    public void displayNext(int next)
    {
        switch (next)
        {
            case (int)BUTTONS.X:
                {
                    guiButtons.chooseX();
                    break;
                }
            case (int)BUTTONS.Y:
                {
                    guiButtons.chooseY();
                    break;
                }
            case (int)BUTTONS.A:
                {
                    guiButtons.chooseA();
                    break;
                }
            case (int)BUTTONS.B:
                {
                    guiButtons.chooseB();
                    break;
                }
        }
    }


    public void startGame()
    {
        guiButtons.activate();
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
        guiButtons.reset();
        if (input == pattern[correctInput])
        {
            playRandomCorrectSound();
            correctInput++;
            if (correctInput > length)
            {
                complete();

            }
            else
            {
                displayNext(pattern[correctInput]);
                anim.SetTrigger("pass");
            }


        }
        else
        {
            correctInput = 0;
            displayNext(pattern[correctInput]);
            anim.SetTrigger("fail");
            sound.clip = incorrect;
            sound.Play();
        }
    }

    public void playRandomCorrectSound()
    {
        sound.clip = correctNoises[Random.Range(0, correctNoises.Length)];
        sound.Play();
    }

    public void complete()
    {
        model.SetActive(false);
        guiButtons.deactivate();
        correctInput = 0;
        GameManager.instance.completedMinigame();
        GameObject particles = Instantiate(Resources.Load("MinigameParticles"), gameObject.transform.position, Quaternion.identity, transform) as GameObject;
        particles.transform.localScale = transform.lossyScale;
        Invoke("destroyObject", 2);

    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }

    public void endGame()
    {
        guiButtons.reset();
        guiButtons.deactivate();
    }
}
