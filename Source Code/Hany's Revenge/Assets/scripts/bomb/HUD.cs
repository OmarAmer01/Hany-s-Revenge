using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bombUI;
    public Text typeBarText;
    public Text tries;
    public static bool bombUIactive;
    public static bool canMove;
    string bombCodeGuess = "";


    public void showBombHUD()
    {
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        bombUI.SetActive(true);
        bombUIactive = true;
    }

    public void hideBombHUD()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        bombUI.SetActive(false);
        bombUIactive = false;
    }

    public void writeButtonToBombCodeGuess(string digit)
    {
        if (bombCodeGuess.Length == 0 && digit == "b")
        {
            return;
        }
        if (digit == "b" && bombCodeGuess.Length > 0)
        {
            bombCodeGuess = bombCodeGuess.Substring(0, bombCodeGuess.Length - 1);
            typeBarText.text = bombCodeGuess;
            return;
        }
        bombCodeGuess = bombCodeGuess + digit;
        typeBarText.text = bombCodeGuess;
    }

    void Start()
    {
        canMove = true;
        bombUI.SetActive(false);
        bombUIactive = false;
    }
    void Update()
    {
        // tries.text = tries.text.Substring(0, tries.text.Length);

        //  tries.text = tries.text.Insert(tries.text.Length,  defuse.defuseAttemptsLeft.ToString());
        tries.text = "Tries Left: "+(defuse.defuseAttemptsLeft + 1).ToString();
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);



        if (Input.GetKeyDown(KeyCode.F))
        {
            //  Debug.Log("F Pressed " + hit.transform.name);
        }
        if (!bombUI.activeInHierarchy && Input.GetKeyDown(KeyCode.F) && hit.transform.name == "Bomb")
        {
            showBombHUD();
            return;

        }
        if (bombUI.activeInHierarchy && Input.GetKeyDown(KeyCode.F) && hit.transform.name == "Bomb")
        {
            hideBombHUD();
            return;
        }
    }
}
