using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueHandler : MonoBehaviour
{
    public ChoiceOption openingChoice;
    public DialoguePath currentDialogue;

    public AudioHandler audioHandler;
    public CameraLook cameraLook;
    public ChoiceOption currentChoice;
    [SerializeField] GameObject blanc;
    [SerializeField] List<GameObject> blancPoses = new List<GameObject>();
    Speaker currentSpeaker;

    [Header("UI Elements")]
    public List<Button> textChoices = new List<Button>();
    public float dialogueSpeed;
    public TextMeshProUGUI dialogueText;
    public GameObject timerObject;

    public bool startInterview = false;
    public DialoguePath interviewStart;
    public DialoguePath finaleStart;
    public VariableHandler variableHandler;
    public BlackScreenPopup blackScreenScript;
    public GameObject blackScreen;
    bool hasPlayed = false;
    public TextMeshProUGUI screenText;
    public Camera fightCam;
    [SerializeField] Camera mainCam;
    Image timerImage;
    bool blackScreenEnabled = false;
    [SerializeField] bool isChoice = false;
    [SerializeField] AudioSource soundEffectsSource;
    public bool isTyping = false;
    bool finaleStarted = false;
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
        VariableChecks();
        HandleSFX();
        if (currentDialogue != null && !isChoice)
        {
            if (currentDialogue.enableDeathScreen && blackScreenEnabled == false)
            {   
                blackScreenScript.EnableBlackScreen(blackScreen, screenText, currentDialogue);
                blackScreenEnabled = true;
            }
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
        
        if (FindObjectOfType<InteractableObject>() == null && currentDialogue == null && finaleStarted == false)
        {
            StartCoroutine(BeginFinale());
            finaleStarted = true;
        }
    }

    public void PlayDialogue(DialoguePath dialogue)
    {
        if (!isChoice)
        {
            currentSpeaker = dialogue.speaker[dialogue.lineIndex];
            dialogueText.color = currentSpeaker.speakerColor;
            dialogueText.font = currentSpeaker.speakerFont;
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (dialogueText.text == dialogue.dialogue[dialogue.lineIndex])
                {
                    PlayNextLine();
                }
                else
                {
                    dialogueText.text = dialogue.dialogue[dialogue.lineIndex];
                    StopAllCoroutines();
                }
            }
            if (currentDialogue != null)
            {
                if (currentDialogue.poses[currentDialogue.lineIndex] != null && !isTyping)
                {
                    foreach (GameObject pose in blancPoses)
                    {
                        if (pose.name == currentDialogue.poses[currentDialogue.lineIndex].name)
                        {
                            pose.SetActive(true);
                        }
                        else
                        {
                            pose.SetActive(false);
                        }
                    }
                }
            }

            if (!isTyping)
            {
                StartCoroutine(Typewriter(dialogue));
            }
        }  
    }

    public IEnumerator Typewriter(DialoguePath dialoguePath)
    {
        if (dialogueText.text != dialoguePath.dialogue[dialoguePath.lineIndex])
        {
            isTyping = true;
            foreach (char letter in dialoguePath.dialogue[dialoguePath.lineIndex].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueSpeed);
            }   
        }
        else
        {
            isTyping = false;
        }
    }

    public IEnumerator BeginInterview()
    {
        currentDialogue = null;
        yield return new WaitForSeconds(0.2f);
        currentDialogue = interviewStart;
        startInterview = true;
        isTyping = false;
        currentDialogue.lineIndex = 0;
        PlayDialogue(currentDialogue);
    }

    public IEnumerator BeginFinale()
    {
        currentDialogue = null;
        cameraLook.enabled = false;
        cameraLook.ChangeLockState(false);
        yield return new WaitForSeconds(3f);
        currentDialogue = finaleStart;
        isTyping = false;
        Typewriter(currentDialogue);
        currentDialogue.lineIndex = 0;
    }

    public void PlayNextLine()
    {
        if (currentDialogue.lineIndex < currentDialogue.dialogue.Count - 1)
        {            
            currentDialogue.lineIndex++;
            isTyping = false;
            dialogueText.text = string.Empty;
        }
        else
        {
            if (currentDialogue.startInterview && !startInterview)
            {
                StartCoroutine(BeginInterview());
                dialogueText.text = string.Empty;
                isChoice = false;
            }
            if (!currentDialogue.startInterview && !currentDialogue.transitionToInterview)
            {
                cameraLook.enabled = false;
                cameraLook.ChangeLockState(false);
                currentChoice = currentDialogue.choice;
                PlayChoice(textChoices, currentChoice);
            }
            else if (currentDialogue.transitionToInterview)
            {
                cameraLook.enabled = true;
                cameraLook.ChangeLockState(true);
                dialogueText.text = string.Empty;
                currentDialogue = null;
            }
        }
    }

    public void PlayChoice(List<Button> choiceText, ChoiceOption choice)
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
        currentDialogue.updatedBools = false;
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
            audioHandler.music.Stop();
            audioHandler.heartbeatPitch.Stop();
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

    public void HandleSFX()
    {
        if (currentDialogue != null)
        {
            if (currentDialogue.soundEffects[currentDialogue.lineIndex] != null && hasPlayed == false)
            {
                soundEffectsSource.clip = currentDialogue.soundEffects[currentDialogue.lineIndex];
                if (!soundEffectsSource.isPlaying)
                {
                    hasPlayed = true;
                    soundEffectsSource.Play();
                }
            }
            else if (currentDialogue.soundEffects[currentDialogue.lineIndex] == null)
            {
                hasPlayed = false;
            }
        }
    }
    public void VariableChecks()
    {
        if (currentDialogue != null && !currentDialogue.updatedBools)
        {
            variableHandler.failures += currentDialogue.increaseFailure;
            if (currentDialogue.enableGunPath)
            {
                variableHandler.gunPathEnabled = true;
            }
            if (currentDialogue.shootLeg)
            {
                if (variableHandler.bothLegsShot)
                {
                    variableHandler.dead = true;
                }
                else if (variableHandler.leftLegShot)
                {
                    variableHandler.bothLegsShot = true;
                }
                else if (variableHandler.noLegsShot)
                {
                    variableHandler.leftLegShot = true;
                }
            }
            if (currentDialogue.startFight)
            {
                mainCam.enabled = false;
                fightCam.enabled = true;
            }
            currentDialogue.updatedBools = true;
        }
    }
}
