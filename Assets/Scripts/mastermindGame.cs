using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mastermindGame : MonoBehaviour, minigame
{
    public int length;
    public GameObject buttons;
    public AudioClip[] phoneClips;
    public AudioClip[] playerClips;
    public AudioClip wrongClip;
    public GameObject model;

    private int correctInput;
    private List<int> pattern;
    private Animator anim;
    private bool canInput;
    private const float animationLength = 1f;
    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        canInput = false;
        anim = GetComponent<Animator>();
    }

    public void complete()
    {
        buttons.SetActive(false);
        model.SetActive(false);
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
        buttons.SetActive(false);
        StopAllCoroutines();
    }

    public void playerInput(int input)
    {
        if (!canInput)
            return;
        if (input == pattern[correctInput])
        {
            playSound(playerClips[input]);
            correctInput++;
            if (correctInput >= length)
            {
                complete();

            }

            anim.SetTrigger("pass");
        }
        else
        {

            correctInput = 0;
            anim.SetTrigger("fail");
            StartCoroutine(displayPattern(true));
        }
    }

    public void startGame()
    {
        buttons.SetActive(true);
        pattern = new List<int>();
        correctInput = 0;
        for (int i = 0; i < length; i++)
        {
            pattern.Add(Random.Range(0, 4));
        }
        StartCoroutine(displayPattern(false));
    }

    public void playSound(AudioClip a)
    {
        sound.clip = a;
        sound.Play();
    }

    public IEnumerator displayPattern (bool incorrect)
    {

        WaitForSeconds wait = new WaitForSeconds(animationLength);
        canInput = false;
        if (incorrect)
        {
            playSound(wrongClip);
            yield return wait; 
        }

        foreach (int current in pattern)
        {
            switch(current)
            {
                case (int)BUTTONS.A:
                    anim.SetTrigger("A");
                    playSound(phoneClips[2]);
                    break;
                case (int)BUTTONS.B:
                    anim.SetTrigger("B");
                    playSound(phoneClips[3]);
                    break;
                case (int)BUTTONS.X:
                    anim.SetTrigger("X");
                    playSound(phoneClips[0]);
                    break;
                case (int)BUTTONS.Y:
                    anim.SetTrigger("Y");
                    playSound(phoneClips[1]);
                    break;
            }
            yield return wait;


        }
        canInput = true;
    }
}
