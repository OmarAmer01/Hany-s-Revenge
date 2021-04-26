using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayCountDown : MonoBehaviour
{


    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        string adjustedSecs = "N/A";
        string adjustedMins = "N/A";
        int bombTime = -1;
        bombTime = (int)bombTimer.currTime;
        int mins = bombTime / 60;
        int secs = bombTime % 60;
        if (secs < 10)
        {
             adjustedSecs = "0" + secs;
        } else
        {
            adjustedSecs = secs.ToString();
        }

        if (mins < 10)
        {
            adjustedMins = "0" + mins;
        }
        else
        {
            adjustedMins = mins.ToString();
        }


        string countDown = adjustedMins + " : " + adjustedSecs;
        GetComponent<Text>().text = countDown;
    }
}
