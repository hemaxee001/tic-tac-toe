using System;
using System.Drawing;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;


public class sscore_ : MonoBehaviour
{
    GridLayoutGroup gridLayoutGroup;
    public int size;
    public GameObject Buttonprefab; // button prifab
    //public Transform grid; // grid
    public RectTransform grid; // grid
    public Button[,] buttons;
    public Text turn; // X and Oturn button
    public Text winText; // check win
    public Text OScore;
    public Text XScore;
    private int Counter = 0;
    public int check = 3;
    int Score_O = 0;
    int Score_X = 0;

    /*
        1000 * .80 = 800
        1000 * .02
        1000 * 2 / 100

        X, Y, Z
        {x,y,z}

        Vector3
        -> x,y,z
        Vector2
        -> x,y
     */

    void Start()
    {
        gridLayoutGroup = grid.GetComponent<GridLayoutGroup>();
        //RectTransform rectTransform = grid.GetComponent<RectTransform>();
        Vector2 gridSize = grid.sizeDelta;
        float buttonSize = (gridSize.x * .85f) / size;
        gridLayoutGroup.cellSize = new Vector2(buttonSize, buttonSize);
        float spacingSize = (gridSize.x * .125f) / size;
        gridLayoutGroup.spacing = new Vector2(spacingSize, spacingSize);

        buttons = new Button[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject clone = Instantiate(Buttonprefab, grid);

                Button h = clone.GetComponent<Button>();
                Text t = clone.GetComponentInChildren<Text>();


                buttons[i, j] = h;

                h.onClick.AddListener(() =>
                {
                    if (winText.text != "")
                    {
                        return;
                    }
                    if (t.text == "")
                    {
                        //t.text = Counter % 2 != 0 ? "X" : "O";
                        //turn.text = Counter % 2 != 0 ? "O TURN" : "X TURN";
                        t.text = Counter % 2 == 0 ? "O" : "X";
                        turn.text = Counter % 2 == 0 ? "X TURN" : "O TURN";
                        Counter++;
                        WinCheck();
                    }
                });
            }
        }
    }



    //---------------------------------------------reverse-------------------------------------------------------------
    public void Reset_bt()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                buttons[i, j].GetComponentInChildren<Text>().text = "";
            }
        }
        Counter = 0;
        winText.text = "";
        OScore.text = "0";
        XScore.text = "0";
        turn.text = "X TURN";
        Score_O = 0;
        Score_X = 0;
    }

    //--------------------------------------------Wincheck-----------------------------------------------
    void WinCheck()
    {
        int Ocounter = 0;
        int Xcounter = 0;
        //==========================row==========================
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size - check + 1; j++)
            {
                Ocounter = 0;
                Xcounter = 0;

                for (int k = j; k < j + check; k++)
                {
                    Text t = buttons[i, k].GetComponentInChildren<Text>();
                    if (t.text == "O")
                    {
                        Ocounter++;
                    }
                    else if (t.text == "X")
                    {
                        Xcounter++;
                    }
                }
                if (Ocounter == check)
                {
                    Score_O++;
                    OScore.text = Score_O.ToString();
                    for (int k = j; k < j + check; k++)
                    {
                        Text t = buttons[i, k].GetComponentInChildren<Text>();
                        t.text = "";
                    }
                }
                else if (Xcounter == check)
                {
                    Score_X++;
                    XScore.text = Score_X.ToString();
                    for (int k = j; k < j + check; k++)
                    {
                        Text t = buttons[i, k].GetComponentInChildren<Text>();
                        t.text = "";

                    }
                }
            }
        }
        //if (Ocounter == size || Xcounter == size)
        //{
        //    Score_O += Ocounter == size ? 1 : 0;
        //    Score_X += Xcounter == size ? 1 : 0;
        //    OScore.text = Score_O.ToString();
        //    XScore.text = Score_X.ToString();
        //    for (int j = 0; j < size; j++)
        //    {
        //        Text t = buttons[i, k].GetComponentInChildren<Text>();
        //        t.text = "";
        //    }
        //}
        //====================================column=============================
        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < size - check + 1; i++)
            {
                Ocounter = 0;
                Xcounter = 0;
                for (int k = i; k < i + check; k++)
                {
                    Text t = buttons[k, j].GetComponentInChildren<Text>();
                    if (t.text == "O") Ocounter++;
                    else if (t.text == "X") Xcounter++;
                }
                if (Ocounter == check)
                {
                    Score_O++;
                    OScore.text = Score_O.ToString();
                    for (int k = i; k < i + check; k++)
                    {
                        Text t = buttons[k, j].GetComponentInChildren<Text>();
                        t.text = "";

                    }
                }
                else if (Xcounter == check)
                {
                    Score_X++;
                    XScore.text = Score_X.ToString();
                    for (int k = i; k < i + check; k++)
                    {
                        Text t = buttons[k, j].GetComponentInChildren<Text>();
                        t.text = "";

                    }
                }
            }

        }
        //if (Ocounter == size)
        //{
        //    Score_O++;
        //    OScore.text = Score_O.ToString();
        //    winText.text = "O is Win";
        //}
        //else if (Xcounter == size)
        //{
        //    Score_X++;
        //    XScore.text = Score_X.ToString();
        //    winText.text = "X is Win";
        //}
        //======================primary diagonal===============================
        /*
        00 11 22 
        01 12 23
        10 21 32
        11 22 33
         */
        for (int i = 0; i < size - check + 1; i++)
        {
            for (int j = 0; j < size - check + 1; j++)
            {
                Xcounter = 0;
                Ocounter = 0;
                for (int k = 0; k < check; k++)
                {
                    Text t = buttons[i + k, k + j].GetComponentInChildren<Text>();
                    if (t.text == "X") Xcounter++;
                    else if (t.text == "O") Ocounter++;
                }
                if (Ocounter == check)
                {
                    Score_O++;
                    OScore.text = Score_O.ToString();
                    for (int k = 0; k < check; k++)
                    {
                        Text t = buttons[i + k, k + j].GetComponentInChildren<Text>();
                        t.text = "";

                    }
                }
                else if (Xcounter == check)
                {
                    Score_X++;
                    XScore.text = Score_X.ToString();
                    for (int k = 0; k < check; k++)
                    {
                        Text t = buttons[i + k, k + j].GetComponentInChildren<Text>();
                        t.text = "";

                    }
                }

            }
        }

        //======================secondry diagonal=================================================
        /*
       02 11 20
       03 12 21
       12 21 30
       13 22 31
       */
        for (int i = 0; i < size - check + 1; i++)
        {
            for (int j = 0; j < size - check + 1; j++)
            {
                Xcounter = 0;
                Ocounter = 0;
                for (int k = 0; k < check; k++)
                {
                    Text t = buttons[i + k, size - j - k - 1].GetComponentInChildren<Text>();
                    if (t.text == "X") Xcounter++;
                    else if (t.text == "O") Ocounter++;
                }
                if (Ocounter == check)
                {
                    Score_O++;
                    OScore.text = Score_O.ToString();
                    for (int k = 0; k < check; k++)
                    {
                        Text t = buttons[i + k, size - j - k - 1].GetComponentInChildren<Text>();
                        t.text = "";
                    }
                }
                else if (Xcounter == check)
                {
                    Score_X++;
                    XScore.text = Score_X.ToString();
                    for (int k = 0; k < check; k++)
                    {
                        Text t = buttons[i + k, size - j - k - 1].GetComponentInChildren<Text>();
                        t.text = "";
                    }
                }
            }
        }
        displayWin();
    }
    public void displayWin()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Text t = buttons[i, j].GetComponentInChildren<Text>();
                if (t.text == "")
                {
                    return;
                }
               
            } 
        }
        if (Score_O > Score_X)
        {
            winText.text = "O is Win";
        }
        else if (Score_X > Score_O)
        {
            winText.text = "X is Win";
        }
        else
        {      
        winText.text = "game over";
        }

    }
}
//if (Score_O > Score_X)
//{
//    winText.text = "O is Win";
//}
//else if (Score_X > Score_O)
//{
//    winText.text = "X is Win";
//}
//else if (Counter == size * size)
//{
//    winText.text = "DRAW";
//}
//else
//{
//    winText.text = "";
//}
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