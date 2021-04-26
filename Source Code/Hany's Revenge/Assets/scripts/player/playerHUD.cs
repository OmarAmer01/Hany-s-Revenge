using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHUD : MonoBehaviour
{
    public Text toolTip;
    public GameObject interactPrompt;
    public float lookDistance = 2 ;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, lookDistance) && hit.transform.tag !="roomStructure")
        {
            if(hit.transform.name != "Player")
            toolTip.text = hit.transform.name;
            interactPrompt.SetActive(false);
            if (hit.transform.tag == "interactable")
            {
                interactPrompt.SetActive(true);
            }
        } else
        {
            toolTip.text = string.Empty;
            interactPrompt.SetActive(false);
        }
        
    }
}
