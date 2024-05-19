using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu()]
public class ChoiceOption : ScriptableObject
{
    [Header("Choice Options [MANDATORY]")]
    public List<string> options = new List<string>();
    public List<DialoguePath> optionPathways = new List<DialoguePath>();
    
    [Header("Timer Options [OPTIONAL]")]
    public bool isTimed = false;
    
    [Range(0f, 10f)] 
    public float timerAmount;
    public DialoguePath outOfTimeDialogue;

    [Header("Enable Black Screen [FOR INTROS AND OUTROS]")]
    public bool enableBlackScreen;
    public bool enableBlackScreenOnTimer;
    public List<int> blackScreenOptions = new List<int>();
    public DialoguePath GetChoice(int choiceOption)
    {
        Debug.Log(choiceOption);
        DialoguePath chosenChoice = optionPathways[choiceOption];
        return chosenChoice;
    }
}

