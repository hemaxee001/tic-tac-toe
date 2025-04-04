using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
public class three_by_three : MonoBehaviour
{
    public int size = 3;
    public GameObject buutonprefab;
    public Transform grid;
    public Text turn;
    public Text winText; 
    
    public Button[,] buttons ;
    private int counter = 0;
    //----------------------start method---------------------------------------------------
        void Start()
        {
            
        buttons = new Button[size,size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject clone = Instantiate(buutonprefab,grid);
                Button b = clone.GetComponent<Button>();
                Text t = clone.GetComponentInChildren<Text>();
                buttons[i,j] = b;
              
                b.onClick.AddListener(() =>
                {
                    if(winText.text != "")
                    {
                        return;
                    }
                    if (t.text == "")
                    {
                        t.text = counter % 2 == 0 ? "O" : "X";
                        turn.text = counter % 2== 0 ? "X TURN" : "O TURN";
                        counter++;
                        WinCheck();
                    }
                });      
            }   
        } 
    }
    //----------------------------reset button-------------------------------------------
    public void Reverse()
    {
        for(int i = 0; i<size;i++)
        {
            for (int j = 0; j < size; j++)
            {
                buttons[i, j].GetComponentInChildren<Text>().text = "";
            }
        }
        counter = 0;
        winText.text = "";
        turn.text = "X TURN";
    }
    //--------------------------wincheck method-------------------------------------
    void WinCheck()
    {
        /*
         00 01 02
         10 11 12
         20 21 22
         */
        //--------------------------row----------------------------------------
            int oCounter = 0;
            int xCounter = 0;
        for(int i = 0; i< size; i++)
        {
             oCounter = 0;
             xCounter = 0;
            for (int j = 0; j < size; j++)
            {
                Text t = buttons[i, j].GetComponentInChildren<Text>();
                if(t.text == "O") oCounter++;
                else if (t.text == "X") xCounter++;
            }
            if (oCounter == size)
            {
                winText.text = "O is Win";
               // print("O is Win");
              
            }
            else if (xCounter == size)
            {
                winText.text = "X is Win";
                //print("X is Win");
              
            }
        }
        //-----------------------column----------------------------

        for(int i = 0; i < size; i++)
        {
             oCounter = 0;
             xCounter = 0;
            for(int j=0 ; j<size ; j++)
            {
                Text t = buttons[j, i].GetComponentInChildren<Text>();
                if(t.text == "O") oCounter ++;
                else if (t.text == "X") xCounter ++;
            }
            if (oCounter == size)
            {
                winText.text = "O is Win";

            }
            else if (xCounter == size)
            {
                winText.text = "X is Win";
            }
        }
        // anty diagonal

        oCounter = 0;
        xCounter= 0;
        for (int i = 0; i < size; i++)
        {
            Text t = buttons[i, i].GetComponentInChildren<Text>();
            if (t.text == "O") oCounter++;
            if (t.text == "X") xCounter++;
        }
        if (oCounter == size)
        {
            winText.text = "O is Win";
        }
        else if (xCounter == size)
        {
            winText.text = "X is Win";
        }

        //----diagonal
        oCounter = 0;
        xCounter = 0;
        /*
         size = 3 - 0 - 1
               = 0,2
        size = 3 - 1 - 1
               = 1,1
        size = 3 - 2 - 1
               = 2,0
            
        */
        for (int i = 0; i < size; i++)
        {
            Text t = buttons[i, size-i-1].GetComponentInChildren<Text>();
            if (t.text == "O") oCounter++;
            if (t.text == "X") xCounter++;
        }
        if (oCounter == size)
        {
            winText.text = "O is Win";
        }
        else if (xCounter == size)
        {
            winText.text = "X is Win";
        }
        if(counter == 9 && winText.text == "")
        {
            winText.text = "DRAW";
        }
        //if (Ocounter == size || Xcounter == size)
        //{
        //    Score_O += Ocounter == size ? 1 : 0;
        //    Score_X += Xcounter == size ? 1 : 0;
        //    OScore.text = Score_O.ToString();
        //    XScore.text = Score_X.ToString();
        //    for (int i = 0; i < size; i++)
        //    {
        //        Text t = buttons[i, size - i - 1].GetComponentInChildren<Text>();
        //        t.text = "";
        //    }
        //}
    }


}

