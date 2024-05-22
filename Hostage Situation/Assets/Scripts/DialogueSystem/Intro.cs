using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Intro : MonoBehaviour
{
    [SerializeField] BlackScreenPopup blackScreenPopup;
    [SerializeField] GameObject blackScreen;
    [SerializeField] TextMeshProUGUI blackScreenText;
    [SerializeField] DialoguePath introDialogue;
    [SerializeField] DialogueHandler dialogueHandler;
    // Start is called before the first frame update
    void Start()
    {
        blackScreenPopup.EnableBlackScreen(blackScreen, blackScreenText, introDialogue);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            blackScreenPopup.DisableBlackScreen(blackScreen, blackScreenText);
            dialogueHandler.enabled = true;
            this.enabled = false;
        }
    }
}
