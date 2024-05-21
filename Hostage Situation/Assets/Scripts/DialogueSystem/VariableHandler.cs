using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableHandler : MonoBehaviour
{
    // PLEASE DO NOT VIEW THIS SCRIPT JESUS CHRIST IT IS AWFUL CODE. I DIDN'T HAVE ENOUGH TIME TO CODE A DYNAMIC VARIABLE HANDLER.
    public int failures;
    public bool leftLegShot;
    public bool bothLegsShot;
    public bool gunPathEnabled;
    public bool dead;
    public int interviewObjects;
    
    public DialogueHandler dialogueHandler;
    public DialoguePath questionMarkPath, spotlightPath, emptySeatsPath;
    public ChoiceOption attackChoice, gunPathBothLegs, humiliateChoice;
    public ChoiceOption chargeChoice, noLegsShot, oneLegShot;
    public List<DialoguePath> questionMarkFailure = new List<DialoguePath>();
    public List<DialoguePath> spotlightFailurePath = new List<DialoguePath>();
    public List <DialoguePath> emptySeatsFailurePath = new List<DialoguePath>();

    void Update()
    {
        if (dialogueHandler.currentDialogue == questionMarkFailure[0])
        {
            dialogueHandler.currentDialogue = questionMarkPath;
            dialogueHandler.currentDialogue.lineIndex = 0;
        }
        if (dialogueHandler.currentDialogue == spotlightFailurePath[0])
        {
            dialogueHandler.currentDialogue = spotlightPath;
            dialogueHandler.currentDialogue.lineIndex = 0;
        }
        if (dialogueHandler.currentDialogue == questionMarkFailure[0])
        {
            dialogueHandler.currentDialogue = questionMarkPath;
            dialogueHandler.currentDialogue.lineIndex = 0;
        }
        if (failures == 1)
        {
            questionMarkPath = questionMarkFailure[0];
            spotlightPath = spotlightFailurePath[0];
            emptySeatsPath = emptySeatsFailurePath[0];
        }
        else if (failures == 2)
        {
            questionMarkPath = questionMarkFailure[1];
            spotlightPath = spotlightFailurePath[1];
            emptySeatsPath = emptySeatsFailurePath[1];
        }
        else if (failures >= 3 && !leftLegShot)
        {
            questionMarkPath = questionMarkFailure[2];
            spotlightPath = spotlightFailurePath[2];
            emptySeatsPath = emptySeatsFailurePath[2];
            chargeChoice = noLegsShot;
        }
        else if (failures >= 3 && leftLegShot && !bothLegsShot)
        {
            questionMarkPath = questionMarkFailure[3];
            spotlightPath = spotlightFailurePath[3];
            emptySeatsPath = emptySeatsFailurePath[3];
            chargeChoice = oneLegShot;
        }
        else if (failures >= 3 && dead)
        {
            questionMarkPath = questionMarkFailure[4];
            spotlightPath = spotlightFailurePath[4];
            emptySeatsPath = emptySeatsFailurePath[4];     
        }

        if (gunPathEnabled && !leftLegShot && dialogueHandler.currentChoice == attackChoice)
        {
            dialogueHandler.currentChoice = gunPathBothLegs; 
            dialogueHandler.PlayChoice(dialogueHandler.textChoices, dialogueHandler.currentChoice);
        }
        else if (!bothLegsShot && dialogueHandler.currentChoice == attackChoice)
        {
            dialogueHandler.currentChoice = humiliateChoice; 
            dialogueHandler.PlayChoice(dialogueHandler.textChoices, dialogueHandler.currentChoice);
        }
    }
}
