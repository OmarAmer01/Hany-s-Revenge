using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tvTurnOn : MonoBehaviour
{
     static bool tvOn;
    public GameObject player;
    private void Start()
    {
        tvOn = false;
    }
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(player.transform.position, player.transform.forward, out hit, 2);
       if(hit.transform == null)
        {
            return;
        }
      
        if (hit.transform.name == "TV" && Input.GetKeyDown(KeyCode.F) && !tvOn)
        {
            Debug.Log("TV ON");
            tvOn = true;
            return;
        } 
        if(hit.transform.name == "TV" && Input.GetKeyDown(KeyCode.F) && tvOn)
        {
            Debug.Log("TV OFF");
            tvOn = false;
            return;
        }

    }
}
