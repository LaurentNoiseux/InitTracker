using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Controller : MonoBehaviour {
    
    public Button buttonRead;
    public Button buttonNext, buttonPrevious;
    public Button buttonNew, buttonClear, buttonPanelAddConfirm,
        buttonAction, buttonDamage, buttonHealing, buttonConfirmAction,
        buttonAddCancel, buttonActionCancel;
    public InputField inputName, inputInit, inputHp, inputDamage;
    public Canvas canvas1;
    public Dropdown dropdown1;
    public Text firstText, secondText, thirdText, fourthText, fifthText, sixthText, 
        seventhText, eighthText, ninthText, tenthText, eleventhText, twelfthText,
        thirtheenthText, fourtheenthText, fiftheenthText, sixtheenthText, seventheenthText,
        eighteenthText, nintheenthText, twentiethText;
    public GameObject panelAddCreatures, panelAction;

    private int highestInit, lowestInit, playingCreaturePosition = 0;
    private List<Creature> creatureArray;
    private List<Text> textArray;

    // Use this for initialization
    void Start() {
		buttonPrevious.onClick.AddListener(PreviousCreature);
        buttonNext.onClick.AddListener(NextCreature);
        buttonPanelAddConfirm.onClick.AddListener(NewCreatureListener);
        buttonClear.onClick.AddListener(ButtonClear);
        buttonNew.onClick.AddListener(OpenPanelAdd);
        buttonAction.onClick.AddListener(OpenPanelAction);
        buttonDamage.onClick.AddListener(DamageSelected);
        buttonHealing.onClick.AddListener(HealingSelected);
        buttonConfirmAction.onClick.AddListener(ConfirmAction);
        buttonAddCancel.onClick.AddListener(ClosePanelAdd);
        buttonActionCancel.onClick.AddListener(ClosePanelAction);
        buttonRead.onClick.AddListener(Read);

        creatureArray = new List<Creature>();
        textArray = new List<Text>();
        dropdown1.ClearOptions();

        textArray.Add(firstText);
        textArray.Add(secondText);
        textArray.Add(thirdText);
        textArray.Add(fourthText);
        textArray.Add(fifthText);
        textArray.Add(sixthText);
        textArray.Add(seventhText);
        textArray.Add(eighthText);
        textArray.Add(ninthText);
        textArray.Add(tenthText);
        textArray.Add(eleventhText);
        textArray.Add(twelfthText);
        textArray.Add(thirtheenthText);
        textArray.Add(fourtheenthText);
        textArray.Add(fiftheenthText);
        textArray.Add(sixtheenthText);
        textArray.Add(seventheenthText);
        textArray.Add(eighteenthText);
        textArray.Add(nintheenthText);
        textArray.Add(twentiethText);
        highestInit = 0;
        lowestInit = 20;
        ClearText(true);
        panelAddCreatures.SetActive(false);
        panelAction.SetActive(false);
        Screen.SetResolution(480, 640, false);
    }

	private void PreviousCreature()
	{
		playingCreaturePosition--;
        if(playingCreaturePosition < 0)
        {
            playingCreaturePosition = creatureArray.Count-1;
        }
        PrintCreatures();
	}
    private void NextCreature()
    {
        playingCreaturePosition++;
        if(playingCreaturePosition >= creatureArray.Count)
        {
            playingCreaturePosition = 0;
        }
        PrintCreatures();
    }
    private void Read()
    {
        string path = "test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            String[] line = reader.ReadLine().Split();
            NewCreature(line[0], int.Parse(line[1]), int.Parse(line[2]));
 
        }
        reader.Close();
    }
    private void NewCreatureListener()
    {
        NewCreature(inputName.text, int.Parse(inputInit.text), int.Parse(inputHp.text));
    }

    private void OpenPanelAdd()
    {
        panelAddCreatures.SetActive(true);
    }
    private void ClosePanelAdd()
    {
        panelAddCreatures.SetActive(false);
    }
    private void OpenPanelAction()
    {
        panelAction.SetActive(true);
    }
    private void ClosePanelAction()
    {
        panelAction.SetActive(false);
    }
    private void ConfirmAction()
    {
        if (creatureArray.Count != 0)
        {
            int damageValue = int.Parse(inputDamage.text);
            int targetPosition = dropdown1.value;

            if (!buttonDamage.IsInteractable())
            {
                damageValue = damageValue * -1;
            }
            creatureArray[targetPosition].setHp(damageValue);
            if (creatureArray[targetPosition].getHp() <= 0) {


                for (int i = targetPosition; i < creatureArray.Count - 1; i++)
                {
                    creatureArray[i] = creatureArray[i + 1];
                }
                creatureArray.RemoveAt(creatureArray.Count - 1);
            }
            PrintCreatures();
        }
        panelAction.SetActive(false);
    }
    private void NewCreature(string name, int init, int hp)
    {
        Creature creature = new Creature(name, init, hp);

        if (creatureArray.Count == 0)
        {
            creatureArray.Add(creature);
            highestInit = creature.getInit();
            lowestInit = creature.getInit();

        }
        else if (creature.getInit() >= highestInit)
        {
            AddTop(creature);
            highestInit = creature.getInit();
        }
        else if(creature.getInit() <= lowestInit)
        {
            creatureArray.Add(creature);
            lowestInit = creature.getInit();
        }
        else 
        {
            AddMiddle(creature);
        }
        PrintCreatures();
        panelAddCreatures.SetActive(false);
    }
    private void AddTop(Creature creature)
    {
        creatureArray.Add(creatureArray[creatureArray.Count - 1]);
        for (int i = creatureArray.Count - 1; i >= 2; i--)
        {
            creatureArray[i - 1] = creatureArray[i - 2];
        }
        creatureArray[0] = creature;
    }
    private void AddMiddle(Creature creature)
    {
        bool positionFound = false;
        int aimedPosition = 0;
        int positionSearch = 1;
        while (!positionFound)
        {
            if (creature.getInit() >= creatureArray[positionSearch].getInit())
            {
                positionFound = true;
                aimedPosition = positionSearch;
            }
            else
            {
                positionSearch++;
            }
        }
        creatureArray.Add(creatureArray[creatureArray.Count - 1]);

        for (int i = creatureArray.Count - 1; i >= 2 + aimedPosition; i--)
        {
            creatureArray[i - 1] = creatureArray[i - 2];
        }
        creatureArray[aimedPosition] = creature;
    }
    void Update() {
        if(creatureArray.Count >= 20)
        {
            buttonNew.interactable = false;
        }
        else
        {
            buttonNew.interactable = true;
        }
    }
   
    private void PrintCreatures()
    {
        dropdown1.ClearOptions();
        ClearText(false);
        for(int i = 0; i < creatureArray.Count; i++)
        {
			creatureArray[i].resetPrintText();
            if (playingCreaturePosition == i)
            {
                creatureArray[playingCreaturePosition].setPrintText("*" + creatureArray[playingCreaturePosition].getPrintText() + "*");
            }
            textArray[i].text = creatureArray[i].getPrintText();
            dropdown1.options.Add(new Dropdown.OptionData(creatureArray[i].getCreatureName()));
        }
    }

    private void ButtonClear()
    {
        ClearText(true);
    }

    private void ClearText(bool totalClear)
    {
        dropdown1.ClearOptions();
        foreach ( Text text in textArray)
        {
            text.text = "";
        }
        if (totalClear)
        {
            creatureArray.Clear();
        }
    }

    private void DamageSelected()
    {
        buttonDamage.interactable = false;
        buttonHealing.interactable = true;
    }
    private void HealingSelected()
    {
        buttonDamage.interactable = true;
        buttonHealing.interactable = false;
    }
}
