using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueHandler : MonoBehaviour
{
    public ChoiceOption openingChoice;
    DialoguePath currentDialogue;
    ChoiceOption currentChoice;
    Speaker currentSpeaker;

    [Header("UI Elements")]
    public List<Button> textChoices = new List<Button>();
    public float dialogueSpeed;
    public TextMeshProUGUI dialogueText;
    public GameObject timerObject;

    public BlackScreenPopup blackScreenScript;

    public GameObject blackScreen;
    public TextMeshProUGUI screenText;

    Image timerImage;

    bool isChoice = false;
    bool isTyping = false;
    float timeScale = 1f;
    void Start()
    {
        dialogueText.text = string.Empty;
        PlayChoice(textChoices, openingChoice);
        timerImage = timerObject.GetComponentInChildren<Image>();
        currentChoice = openingChoice;
    }

    void Update()
    {
        if (currentDialogue != null && !isChoice)
        {
            PlayDialogue(currentDialogue);
        }

        if (isChoice && currentChoice.isTimed)
        {
            timerImage.enabled = true;
            if (timeScale > 0)
            {
                timeScale -= (Time.deltaTime / currentChoice.timerAmount);
                timerObject.transform.localScale = new Vector3(timeScale, 1f, 1f);
            }
            else
            {
                timerObject.transform.localScale = new Vector3(1f, 1f, 1f);
                timerImage.enabled = false;
                FailedChoice(currentChoice.outOfTimeDialogue);
                timeScale = 1f;
            }
        }
    }

    void PlayDialogue(DialoguePath dialogue)
    {
        if (!isChoice)
        {
            currentSpeaker = currentDialogue.speaker[currentDialogue.lineIndex];
            dialogueText.color = currentSpeaker.speakerColor;
            dialogueText.font = currentSpeaker.speakerFont;
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (dialogueText.text == currentDialogue.dialogue[currentDialogue.lineIndex])
                {
                    PlayNextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = currentDialogue.dialogue[currentDialogue.lineIndex];
                }
            }

            if (!isTyping)
            {
                StartCoroutine(Typewriter(currentDialogue));
            }
        }  
    }

    IEnumerator Typewriter(DialoguePath dialoguePath)
    {
        isTyping = true;
        foreach (char letter in dialoguePath.dialogue[dialoguePath.lineIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }   
    }

    void PlayNextLine()
    {
        if (currentDialogue.lineIndex < currentDialogue.dialogue.Count - 1)
        {
            currentDialogue.lineIndex++;
            isTyping = false;
            dialogueText.text = string.Empty;
        }
        else
        {
            currentChoice = currentDialogue.choice;
            PlayChoice(textChoices, currentChoice);
        }
    }

    void PlayChoice(List<Button> choiceText, ChoiceOption choice)
    {   
        isChoice = true;  
        DisplayChoices(choiceText, choice);
    }

    void DisplayChoices(List<Button> choiceObjects, ChoiceOption choice)
    {
        dialogueText.text = string.Empty;
        textChoices[0].gameObject.SetActive(true);
        if (choice.options.Count >= 2)
        {
            textChoices[1].gameObject.SetActive(true);
        }
        if (choice.options.Count == 3)
        { 
            textChoices[2].gameObject.SetActive(true);
        }
        
        int optionArray = 0;
        foreach (Button buttonObjects in choiceObjects)
        {
            if (optionArray >= choice.options.Count)
            {
                break;
            }
            TextMeshProUGUI textObject = buttonObjects.GetComponentInChildren<TextMeshProUGUI>();
            textObject.text = choice.options[optionArray];
            optionArray++;
        }
    }

    public void PickChoice(int choiceNumber)
    {
        DialoguePath newDialogue = currentChoice.GetChoice(choiceNumber);
        currentDialogue = newDialogue;
        isChoice = false;
        isTyping = false;
        currentDialogue.lineIndex = 0;
        foreach (Button buttonObjects in textChoices)
        {
            buttonObjects.gameObject.SetActive(false);
            timerImage.enabled = false;
        }

        if (currentChoice.enableBlackScreen)
        {
            foreach (int option in currentChoice.blackScreenOptions)
            {
                Debug.Log(option);
                if (option == choiceNumber)
                {
                    blackScreenScript.EnableBlackScreen(blackScreen, screenText, currentDialogue);
                }
            }
        }
    }

    public void FailedChoice(DialoguePath failedDialogue)
    {
        currentDialogue = failedDialogue;
        isChoice = false;
        isTyping = false;
        currentDialogue.lineIndex = 0;
        foreach (Button buttonObjects in textChoices)
        {
            buttonObjects.gameObject.SetActive(false);
            timerImage.enabled = false;
        }

        if (currentChoice.enableBlackScreenOnTimer)
        {
            blackScreenScript.EnableBlackScreen(blackScreen, screenText, currentDialogue);
        }
    }
}
