using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature {

    private string CreatureName, printText;
    private int init;
    private int currentHp, maxHp;

    public Creature(string tempCreatureName, int tempInit, int tempHp)
    {
        CreatureName = tempCreatureName;
        init = tempInit;
        currentHp = tempHp;
        maxHp = tempHp;
        printText = CreatureName + " Hp: " + currentHp;
    }
    public void setHp(int value)
    {
        if (value + currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp = currentHp + value;
        }
    }
    public void setPrintText(string newPrint)
    {
        printText = newPrint;
    }
    public string getCreatureName()
    {
        return CreatureName;
    }

    public int getInit()
    {
        return init;
    }
    public int getHp()
    {
        return currentHp;
    }
  
    public string getPrintText()
    {
        return printText;
    }
    public void resetPrintText()
    {
        printText = CreatureName + " Hp: " + currentHp;
    }
}
