using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mastermindGame : MonoBehaviour, minigame
{
    public int length;
    public GameObject buttons;

    private int correctInput;
    private List<int> pattern;
    private Animator anim;
    private bool canInput;
    private const float animationLength = 1f;

    private void Start()
    {
        canInput = false;
        anim = GetComponent<Animator>();
    }

    public void complete()
    {
        GameManager.instance.completedMinigame();
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
            StartCoroutine(displayPattern());
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
        StartCoroutine(displayPattern());
    }

    public IEnumerator displayPattern ()
    {
        canInput = false;
        foreach(int current in pattern)
        {
            switch(current)
            {
                case (int)BUTTONS.A:
                    anim.SetTrigger("A");
                    break;
                case (int)BUTTONS.B:
                    anim.SetTrigger("B");
                    break;
                case (int)BUTTONS.X:
                    anim.SetTrigger("X");
                    break;
                case (int)BUTTONS.Y:
                    anim.SetTrigger("Y");
                    break;
            }
            yield return new WaitForSeconds(animationLength);


        }
        canInput = true;
    }
}
