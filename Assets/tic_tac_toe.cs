using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.UI;

public class tic_tac_toe : MonoBehaviour
{

    public Button[] Buttons = new Button[9];
    int counter = 0;
    public Text WinText;
    public Text turntext;
    public void Start()
    {
        Reset();
    }
    public void buttonClick(int index)
    {

        if (WinText.text != "")
        {
            return;
        }
        Text t = Buttons[index].GetComponentInChildren<Text>();
        if (t.text != "") return;
        t.text = counter % 2 == 0 ? "X" : "O";
        turntext.text = counter % 2 == 0 ? "O TURN " : "X TURN";
        counter++;
        CheckWin();

    }
    public void Reset()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponentInChildren<Text>().text = "";
        }
        counter = 0;
        WinText.text = "";
        turntext.text = "X TURN";
    }
    private void CheckWin()
    {
        Check(0, 1, 2);
        Check(3, 4, 5);
        Check(6, 7, 8);
        Check(0, 3, 6);
        Check(1, 4, 7);
        Check(2, 5, 8);
        Check(0, 4, 8);
        Check(2, 4, 6);
        if (counter == 9 && WinText.text == "")
        {
            WinText.text = "draw";
        }
    }
    private void Check(int i1, int i2, int i3)
    {
        var t1 = Buttons[i1].GetComponentInChildren<Text>();
        var t2 = Buttons[i2].GetComponentInChildren<Text>();
        var t3 = Buttons[i3].GetComponentInChildren<Text>();
        if (t1.text != "" && t1.text == t2.text && t2.text == t3.text)
        {
            WinText.text = t1.text + " is Win ";
            //return true;
        }
        //return false;
    }
}
