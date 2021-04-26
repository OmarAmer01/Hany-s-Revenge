using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombTimer : MonoBehaviour
{
    [SerializeField]
    public static float startTime = 300;
    [SerializeField]
    public static float currTime = 0;
    private void Start()
    {
        currTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currTime -= 1* Time.deltaTime;
       // print(currTime);
    }
}
