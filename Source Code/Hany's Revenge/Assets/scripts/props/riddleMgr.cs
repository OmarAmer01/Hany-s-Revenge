using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
public class riddleMgr : MonoBehaviour
{
    public GameObject mgrObj;
    public GameObject player;
    public Canvas riddleUI;
    public Text riddleText;
    public string objRiddle;
    public char answer;
    public static int paperNumber = 0;
    bool hasRiddle = false;
    static int[] generatedBefore;
    int[] questionGenerated;
    int i;
    void Start()
    {
        int i = 0;
        generatedBefore = new int[2];
        questionGenerated = new int[3];
        //  answer = gameMgr.getRandomHexOneDigit()[0];
        answer = 'N';
        if (hasRiddle)
        {
            paperNumber++;
            objRiddle = makeQuestion();
            addAnswerToCode(paperNumber);


        }
        else
        {
            for (int j = 0; j < 2; j++)
            {
                int rand = UnityEngine.Random.Range(1, 7);
                while (rand == generatedBefore[j])
                {
                    rand = UnityEngine.Random.Range(1, 7);
                }
                objRiddle = gameMgr.generateDudRiddle(gameObject, rand);
                generatedBefore[i] = rand;
                i++;
            }





        }
    }


    void Update()
    {
        Debug.Log(defuse.bombCodeActual);
        //if (riddleUI.enabled == true && Input.GetKeyDown(KeyCode.F))
        //{
        //    riddleUI.enabled = false;
        //    return;
        //}

        RaycastHit hit;
        Physics.Raycast(player.transform.position, player.transform.forward, out hit, 3);


        if (riddleUI.enabled == true && Input.GetKeyDown(KeyCode.F))
        {
            riddleUI.enabled = false;
            Input.ResetInputAxes();
            //    return;
        }

        if (hit.transform == null)
        {
            return;
        }


        if (hit.transform.name == gameObject.name && Input.GetKeyDown(KeyCode.F) && riddleUI.enabled == false)
        {
            Debug.Log(objRiddle);
            riddleText.text = objRiddle;
            riddleUI.enabled = true;
            Input.ResetInputAxes();


        }


    }

    public void addAnswerToCode(int digitLocation)
    {

        char[] ch = defuse.bombCodeActual.ToCharArray();
        ch[digitLocation - 1] = answer;
        defuse.bombCodeActual = new string(ch);
    }

    public void setRiddle(bool riddleActive)
    {
        if (riddleActive)
        {
            hasRiddle = true;
        }
        else
        {
            hasRiddle = false;
        }
    }

    public static string intQuestion(ref char answer, int questionParameter)
    {
        string fullQuestion = string.Empty;
        string question = "What is the interrupt responsible for \r\n";
        string[] intData = {"Changing Graphics Mode","Getting Mouse Information","Getting The Key Pressed","Display A String From Memory Offset"
            ,"Changing Cursor Location","Drawing"};
        string note = "Note from hany: Remember to sum up the digits of the interrupts for example int 33h is 3 + 3 = 6 \r\n Iam So Stupid why am i giving them hints???";

        fullQuestion = question + intData[questionParameter] + "\r\n" + note + "?";

        switch (questionParameter)
        {
            case 0:
                answer = '1';
                break;
            case 1:
                answer = '6';
                break;
            case 2:
                answer = '7';
                break;
            case 3:
                answer = '3';
                break;
            case 4:
                answer = '1';
                break;
            case 5:
                answer = '1';
                break;

        }
        return fullQuestion;
    }

    public string multiplyQuestion(int x, int y, int questionParameter)
    {
        string answerArr = (x * y).ToString("X");
        if(answerArr.Length < questionParameter)
        {
            questionParameter = 1;
        }
        
        string ansSelector = string.Empty;
        switch (questionParameter)
        {
            case 1:
                ansSelector = "FIRST";
                break;
            case 2:
                ansSelector = "SECOND";
                break;
            case 3:
                ansSelector = "THIRD";
                break;
            case 4:
                ansSelector = "FOURTH";
                break;
            default:
                ansSelector = "FIRST";
                questionParameter = 1;
                break;
        }
        string code = @".MODEL SMALL
    .DATA
    X        DB " + x + @"
    Y        DB " + y + @"  
    RESULT DW ? 
    .CODE
    MAIN PROC FAR    
    MOV AX,@DATA
    MOV DS,AX
    MOV AL, X
MOV AH,Y
MUL AH
MOV RESULT,AX
mov ah,4ch
int 21h
MAIN ENDP
END MAIN";
        string question = "What is the " + ansSelector + " digit of the RESULT\r\n";
        // answer = (x * y).ToString()[0];
        answer = answerArr[answerArr.Length - questionParameter];
        return question + code;

    }
    public string calculatorQuestion(int x, int y, int questionParameter)
    {
        string reg = "AX";
        // ax 0
        // bx 1
        // cx 2
        // dx 3
        if (questionParameter == 2)
        {

            reg = "CX";

        }
        else if (questionParameter == 3)
        {

            reg = "DX";
        }
        else if (questionParameter == 0)
        {
            reg = "AX";
        }
        else
        {
            reg = "BX";
        }


        string question = @".model small
.data
    X dw " + x + @"
    Y dw " + y + @"  
.code
main proc far    
mov ax,@data
mov ds,ax
MOV AX,X
MOV BX,Y
S:MOV DX,0
MOV CX,BX
DIV BX
MOV BX,DX
MOV AX,CX
CMP BX,0
JNE S
MOV CX,AX
MOV AX,X
MOV BX,Y
MUL BX
DIV CX

mov ah,4ch
int 21h

endp main
end main
";
        string fullQuestion = "What is the value of " + reg + " after executing the following code:\r\n" + question;

        switch (reg)
        {
            case "AX":
                int temp = x * y;
                switch (temp)
                {
                    case 10:
                        answer = 'A';
                        break;
                    case 11:
                        answer = 'B';
                        break;
                    case 12:
                        answer = 'C';
                        break;
                    case 13:
                        answer = 'D';
                        break;
                    case 14:
                        answer = 'E';
                        break;
                    case 15:
                        answer = 'F';
                        break;


                }
                answer = temp.ToString("X")[0];
                break;
            case "BX":
                answer = y.ToString()[0];
                break;
            case "CX":
                answer = '1';
                break;
            case "DX":
                answer = '0';
                break;

        }

        return fullQuestion;
    }

    public string mcqQuestion(int questionNumber)
    {

        string[] questionArr = { "Increasing the address bus size decreases" , "the addressing mode of mov ax,[cx] is"
        , "The relation between virtual memory vs cache memory looks like the relation between","A 32-bit address bus allows access to a memory of capacity"
        ,"Which of the following, when used in the data section of a MASM program, reserves 40 bytes of RAM(memory)?",
        " What will be the contents of register AL after the following has been executed"+@"
                                                                                            MOV BL, 8C
                                                                                            MOV AL, 7E
                                                                                            ADD AL, BL",
        "What is the largest signed integer that may be stored in 32 bits?",};


        char[] answerArr = { 'D', 'D', 'B', 'D', 'D', 'A', 'C' };
        string[] choiceArr = { "a) the number of addressable locations\r\nb) the bus bandwidth\r\nc) available memory size\r\nd)none of the above",
                            "a) register\r\nb) direct\r\nc) register indirect\r\nd) none of the above","a) arrays vs pointers\r\nb) resources vs performance\r\nc) segment registers vs index registers\r\nd) portability vs availability",
                            "a) 64 MB\r\nb) 16 MB\r\nc) 1 GB\r\nd) 4 GB","a) db 20 DUP (2)\r\nc) db 20 DUP (20)\r\nb) dw 40 DUP (1)\r\nd) dw 20 DUP (1)",
        "a)	0A and carry flag is set \r\nb)	6A and carry flag is set\r\nc)	0A and carry flag is reset 	d)\r\n6A and carry flag is reset",
            "a)	2^(32) - 1\r\nb)2^(32)\r\nc)2^(31) - 1 	d)\r\n2^(31)"};
        answer = answerArr[questionNumber];
        return questionArr[questionNumber] + "\r\n" + choiceArr[questionNumber];
    }
    public string physicalAddressQuestion(int questionParameter, string ss, string sp)
    {
        string ansSelector = string.Empty;
        switch (questionParameter)
        {
            case 1:
                ansSelector = "FIRST";
                break;
            case 2:
                ansSelector = "SECOND";
                break;
            case 3:
                ansSelector = "THIRD";
                break;
            case 4:
                ansSelector = "FOURTH";
                break;
            default:
                ansSelector = "FIRST";
                questionParameter = 1;
                break;
        }

        string question = "If the content of the register SS = " + ss + "H and the content of the register SP= " + sp + "H,\r\nThen The " + ansSelector + " Digit of the Physical address is:\r\n";
        int shiftedSS = Convert.ToInt32(ss + "0", 16);
        int toHexSp = +Convert.ToInt32(sp, 16);
        string charArr = (shiftedSS + toHexSp).ToString("X");
        answer = charArr[charArr.Length - questionParameter];
        return question;
    }
    public string factorialQuestion(int questionParameter, int x)
    {
        string answerArr = string.Empty;
        int fact = 1;
        for (int i = 1; i <= x; i++)
        {
            fact *= i;
        }

        answerArr = fact.ToString("X");
        if (questionParameter > answerArr.Length)
        {
            questionParameter = 1;
        }


        string ansSelector = string.Empty;
        string code = @".MODEL SMALL
.DATA
n1 DB " + x + @"H 
n2 DW ?
.code
MAIN    PROC FAR               
	MOV AX,@DATA
	MOV DS,AX 
	mov al,n1
        DEC n1
lbl:    mov bl,n1
        mul bl
        dec n1
        jnz lbl        
        mov n2,ax
        HLT
MAIN    ENDP
        END MAIN

";
        switch (questionParameter)
        {
            case 1:
                ansSelector = "FIRST";
                break;
            case 2:
                ansSelector = "SECOND";
                break;
            case 3:
                ansSelector = "THIRD";
                break;
            case 4:
                ansSelector = "FOURTH";
                break;
            default:
                ansSelector = "FIRST";
                questionParameter = 1;
                break;
        }

      
        answer = answerArr[answerArr.Length - questionParameter];
        string fullQuestion = "What is the " + ansSelector + " digit of AX after executing the following code?\r\n" + code;
        return fullQuestion;

    }
    public string makeQuestion()
    {

        int parameter;
        int questionSelector = UnityEngine.Random.Range(0, 6);
        int x = UnityEngine.Random.Range(2, 5);
        int y = UnityEngine.Random.Range(2, 9);
        switch (questionSelector)
        {
            case 0:
                parameter = UnityEngine.Random.Range(0, 6);
                objRiddle = intQuestion(ref answer, parameter); /////////// _____>>>>>> keeps making the same riddle
                break;
            case 1:
                parameter = UnityEngine.Random.Range(1, 6);

                objRiddle = multiplyQuestion(x, y, parameter);
                break;
            case 2:
                x = UnityEngine.Random.Range(1, 6);
                y = UnityEngine.Random.Range(1, 4);
                parameter = UnityEngine.Random.Range(0, 3);
                objRiddle = calculatorQuestion(x, y, parameter);
                break;
            case 3:
                parameter = UnityEngine.Random.Range(0, 7);
                objRiddle = mcqQuestion(parameter);
                break;
            case 4:
                parameter = UnityEngine.Random.Range(1, 5);
                string ss = UnityEngine.Random.Range(4369, 65545).ToString("X");
                string sp = UnityEngine.Random.Range(4369, 65545).ToString("X");
                objRiddle = physicalAddressQuestion(parameter, ss, sp);
                break;
            case 5:
                x = UnityEngine.Random.Range(1, 9);
                parameter = UnityEngine.Random.Range(1, 5);
                objRiddle = factorialQuestion(parameter, x);
                break;

            default:
                parameter = UnityEngine.Random.Range(0, 6);
                objRiddle = intQuestion(ref answer, parameter);
                break;

        }
        return objRiddle + "\r\n \r\n Page " + paperNumber.ToString() + " out of 4";
    }


}


