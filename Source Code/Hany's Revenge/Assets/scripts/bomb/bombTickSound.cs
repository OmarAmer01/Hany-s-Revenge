using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombTickSound : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip tickSnd;
    public double tickDuration;
    float thisSec = 0;
    bool first = true;
    bool second = false;
    bool third = false;
    bool fourth = false;
    void Start()
    {

    }

    void Update()
    {
        thisSec = Time.time;
        float currTime = bombTimer.currTime;
        if (currTime < 300 && currTime > 120 && first)
        {
            first = false;
            tickDuration = tickDuration / 2;
        }
        else if (currTime == 120)
        {
            first = false;
            second = true;
        }

        if (currTime < 120 && currTime > 60 && second)
        {
            second = false;
            tickDuration = tickDuration / 4;
        }
        else if (currTime == 60)
        {
            second = false;
            third = true;
        }

        if (currTime < 60 && currTime > 30 && third)
        {
            tickDuration = tickDuration / 8;
            third = false;
        } else if (currTime == 30)
        {
            third = false;
            fourth = true;
        }

        if ((int)currTime <= 30 && currTime >= 0 && fourth)
        {
            tickDuration = tickDuration / 16;
            fourth = false;
        }
        //
        if (Mathf.Round(currTime) % tickDuration == 0 && !audioSrc.isPlaying && thisSec == Time.time && !explode.hasExploded)
        {

            audioSrc.PlayOneShot(tickSnd);

           
        }
        else
        {
           
        }

    }
}
