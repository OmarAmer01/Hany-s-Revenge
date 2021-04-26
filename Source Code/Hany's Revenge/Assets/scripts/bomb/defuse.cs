
using UnityEngine;
using System;
using UnityEngine.UI;


public class defuse : MonoBehaviour
{

    Component bombExplode;
    public Text typeBar;
    public static string bombCodeActual;
    public static int defuseAttemptsLeft = 2;
    public AudioSource audioSrc;
    public static int codeDigitNum = 4;
    public Canvas winUI;
    public GameObject camera;
    public static string decimalToHexadecimal(int dec)
    {
        if (dec < 1) return "0";

        int hex = dec;
        string hexStr = string.Empty;

        while (dec > 0)
        {
            hex = dec % 16;

            if (hex < 10)
                hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
            else
                hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());

            dec /= 16;
        }

        return hexStr;
    }

    private void Start()
    {
        winUI.enabled = false;

        bombCodeActual = "0000";
      

      //  bombCodeActual = bombCodeActual.Substring(0, codeDigitNum);
        //int rand = UnityEngine.Random.Range(0x1111, 0xffff);
        //bombCodeActual = decimalToHexadecimal(rand);
        //for debugging only:
        //bombCodeActual = "1234";

        bombExplode = GetComponent<explode>();
        //

        defuseAttemptsLeft = 2;

    }
    public void defuseBomb()
    {

        string codeGuess = typeBar.text;
       // string trimmedActual = bombCodeActual.Substring(0, codeDigitNum);

        if (codeGuess == bombCodeActual) 
        {
            Debug.Log("BOMB HAS BEEN DEFUSEF COUNTER TERRORISTS WIN");
            winUI.enabled = true;
            camera.GetComponent<AudioListener>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            if (defuseAttemptsLeft == 0)
            {
                bombExplode.BroadcastMessage("explodeBomb");
            }
            defuseAttemptsLeft--;
            Debug.Log("Tries Decremented, Good Luck You  Idiot Tries Remaining: " + defuseAttemptsLeft);

        }


    }
}
