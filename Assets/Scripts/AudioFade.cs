using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{

    public AudioSource fromSource;
    public AudioSource toSource;
    public float fadeDuration;

    //Called for slow fades
    public void fadeSounds()
    {
        StartCoroutine(Fade());
    }

    //called to suddenly change audiosources
    public void suddenChange()
    {
        //change the volumes immediately
        fromSource.volume = 0;
        toSource.volume = 1;
        toSource.Play();

        //switch the to and from for next fade
        AudioSource temp = toSource;
        toSource = fromSource;
        fromSource = temp;
    }


    //slowly fade betwen the two sounds
    public IEnumerator Fade()
    {
        float time = 0;
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fromSource.volume = Mathf.Lerp(1, 0, time / fadeDuration);
            toSource.volume = Mathf.Lerp(0, 1, time / fadeDuration);
            yield return w;
        }
        AudioSource temp = toSource;
        toSource = fromSource;
        fromSource = temp;
    }
}
