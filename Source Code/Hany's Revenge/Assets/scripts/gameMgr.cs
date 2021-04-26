using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class gameMgr : MonoBehaviour
{
    float bombExplodeTimer;
    public GameObject props;
    public Text paper;
    public Canvas riddleUI;
    string[] arr;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        riddleUI.enabled = false;
        bombExplodeTimer = bombTimer.startTime;
        int[] randArr = new int[6];
        // getActiveRiddleProps();
        initRiddlesOnProps();
        arr = getActiveRiddleProps();


    }
    string[] getActiveRiddleProps()
    {


        GameObject child;
        int j = 0;
        int count = 0;
        for (int i = 0; (i <= props.transform.childCount && j <= 7); i++)
        {


            child = props.transform.GetChild(i).gameObject;


            if (child.transform.tag == "interactable")
            {

                count++;

            }




        }
        string[] nameArr = new string[count];
        for (int i = 0; (i <= props.transform.childCount && j <= 7); i++)
        {


            child = props.transform.GetChild(i).gameObject;


            if (child.transform.tag == "interactable")
            {

                nameArr[j] = child.name;

            }




        }
        return nameArr;
    }
    int[] generateSelectionArray()
    {


        int[] randArr = new int[7];
        int sum = 0;
        int rand = 0;
        System.Random r = new System.Random();
        for (int i = 0; i < randArr.Length; i++)
        {
            rand = r.Next(0, 2);
            randArr[i] = rand;

        }
        while (randArr.Sum() <= 4)
        {
            for (int i = 0; i < randArr.Length; i++)
            {
                if (sum == 4)
                {
                    break;
                }
                if (randArr[i] == 0)
                    randArr[i] = 1;
                sum = randArr.Sum();



            }
            if (sum == 4)
            {
                break;
            }
        }
        while (randArr.Sum() >= 5)
        {
            for (int i = 0; i < randArr.Length; i++)
            {
                if (sum == 4)
                {
                    break;
                }
                if (randArr[i] == 1)
                    randArr[i] = 0;
                sum = randArr.Sum();


            }
            if (sum == 4)
            {
                break;
            }
        }


        return randArr;

    }



    void initRiddlesOnProps()
    {

        int[] selectionArr = generateSelectionArray();

        GameObject child;
        riddleMgr riddleMgrObj;
        int j = 0;
        for (int i = 0; (i <= props.transform.childCount && j <= 7); i++)
        {


            child = props.transform.GetChild(i).gameObject;
            riddleMgrObj = child.GetComponent<riddleMgr>();

            if (child.transform.tag == "interactable")
            {


                if (selectionArr[j] == 1)
                {

                    riddleMgrObj.setRiddle(true);
                }

                j++;
            }




        }
    }

    public static string generateDudRiddle(GameObject gameObj,int dudNum)
    {
        string dud = string.Empty;
        string name = gameObj.name;
        string dudOne = "As you take a look inside the " + name + ", you find nothing useful";
        string dudTwo = "Nope, nothing.";
        string dudThree = "There is nothing here";
        string dudFour = "Shopping List: Milk\r\nEggs\r\nCheese\r\nAl Maizidi Reference";
        string dudFive = "Dear Hany,\r\nWe no longer require your services\r\nYOU ARE FIRED\r\nThank you,\r\nAdministration";
        string dudSix = "You didnt find anything useful in the " + name;
        string dudSeven = "You Find a message that says:\r\nHello! The 2nd Digit is 'A'\r\nTrust me\r\nSigned: Not Hany";
        System.Random r = new System.Random();
        
        switch (dudNum)
        {
            case 1:
                dud = dudOne;
                break;
            case 2:
                dud = dudTwo;
                break;
            case 3:
                dud = dudThree;
                break;
            case 4:
                dud = dudFour;
                break;
            case 5:
                dud = dudFive;
                break;
            case 6:
                dud = dudSix;
                break;
            case 7:
                dud = dudSeven;
                break;
            default:
                dud = "There is nothing here";
                break;


        }
        return dud;
    }

  
   

    void Update()
    {
        //   Debug.Log(arr);
    }

    public static string makeQuestion(char answer, GameObject obj, int pageNum)
    {

        string riddle = "When searching the " + obj.transform.name.ToLower() + ", You found a paper signed: Hany, it says the answer is " + answer + "\n page number: " + pageNum.ToString();
        return riddle;
    }

    public static string getRandomHexOneDigit()
    {
        int rand = UnityEngine.Random.Range(0x0, 0xf);
        string hex = rand.ToString();

        switch (rand)
        {
            case 10:
                hex = "A";
                break;
            case 11:
                hex = "B";
                break;
            case 12:
                hex = "C";
                break;
            case 13:
                hex = "D";
                break;
            case 14:
                hex = "E";
                break;
            case 15:
                hex = "F";
                break;
            default:
                hex = rand.ToString();
                break;

        }
        return hex;
    }
     public void restartOffice()
    {
        riddleMgr.paperNumber = 0;
        SceneManager.LoadScene(1);
    }

    public void toMainMenu()
    {
        riddleMgr.paperNumber = 0;
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        riddleMgr.paperNumber = 0;
        Application.Quit(0);
    }
}
