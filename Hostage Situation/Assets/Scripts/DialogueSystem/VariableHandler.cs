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
    
    public DialogueHandler dialogueHandler;
    public DialoguePath questionMarkPath, spotlightPath, emptySeatsPath;
    public ChoiceOption attackChoice, gunPathBothLegs, humiliateChoice;
    public ChoiceOption charge, noLegsShot, oneLegShot;
    public List<DialoguePath> questionMarkFailure = new List<DialoguePath>();
    public List<DialoguePath> spotlightFailurePath = new List<DialoguePath>();
    public List <DialoguePath> emptySeatsFailurePath = new List<DialoguePath>();
    bool questionMarkUpdatedDialogue = false, spotlightUpdatedDialogue = false, emptySeatsUpdatedDialogue = false;
    void Update()
    {
        if (failures == 1)
        {
            questionMarkPath = questionMarkFailure[0];
            spotlightPath = spotlightFailurePath[0];
            emptySeatsPath = emptySeatsFailurePath[0];
            questionMarkPath.updatedBools = false;
            spotlightPath.updatedBools = false;
            emptySeatsPath.updatedBools = false;
        }
        if (failures == 2)
        {
            questionMarkPath = questionMarkFailure[1];
            spotlightPath = spotlightFailurePath[1];
            emptySeatsPath = emptySeatsFailurePath[1];
            questionMarkPath.updatedBools = false;
            spotlightPath.updatedBools = false;
            emptySeatsPath.updatedBools = false;
        }
        if (failures >= 3 && !leftLegShot)
        {
            questionMarkPath = questionMarkFailure[2];
            spotlightPath = spotlightFailurePath[2];
            emptySeatsPath = emptySeatsFailurePath[2];
            charge = noLegsShot;
            questionMarkPath.updatedBools = false;
            spotlightPath.updatedBools = false;
            emptySeatsPath.updatedBools = false;
        }
        else if (failures >= 3 && leftLegShot && !bothLegsShot)
        {
            questionMarkPath = questionMarkFailure[3];
            spotlightPath = spotlightFailurePath[3];
            emptySeatsPath = emptySeatsFailurePath[3];
            charge = oneLegShot;
            questionMarkPath.updatedBools = false;
            spotlightPath.updatedBools = false;
            emptySeatsPath.updatedBools = false;
        }
        else if (failures >= 3 && bothLegsShot)
        {
            questionMarkPath = questionMarkFailure[4];
            spotlightPath = spotlightFailurePath[4];
            emptySeatsPath = emptySeatsFailurePath[4]; 
            questionMarkPath.updatedBools = false;
            spotlightPath.updatedBools = false;
            emptySeatsPath.updatedBools = false;    
        }

        if (dialogueHandler.currentDialogue == questionMarkFailure[0] && questionMarkUpdatedDialogue == false)
        {
            dialogueHandler.currentDialogue = questionMarkPath;
            dialogueHandler.currentDialogue.lineIndex = 0;
            questionMarkUpdatedDialogue = true;
        }
        if (dialogueHandler.currentDialogue == spotlightFailurePath[0] && spotlightUpdatedDialogue == false)
        {
            dialogueHandler.currentDialogue = spotlightPath;
            dialogueHandler.currentDialogue.lineIndex = 0;
            spotlightUpdatedDialogue = true;
        }
        if (dialogueHandler.currentDialogue == emptySeatsFailurePath[0] && emptySeatsUpdatedDialogue == false)
        {
            dialogueHandler.currentDialogue = emptySeatsPath;
            dialogueHandler.currentDialogue.lineIndex = 0;
            emptySeatsUpdatedDialogue = true;
        }
        if (leftLegShot && dialogueHandler.currentChoice == noLegsShot)
        {
            dialogueHandler.currentChoice = oneLegShot;
            dialogueHandler.PlayChoice(dialogueHandler.textChoices, dialogueHandler.currentChoice);
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
