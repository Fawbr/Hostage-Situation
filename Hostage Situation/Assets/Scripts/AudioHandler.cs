using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] DialogueHandler dialogueHandler;
    [SerializeField] VariableHandler variableHandler;
    [SerializeField] public AudioSource heartbeatPitch;
    [SerializeField] public AudioSource music;

    // Update is called once per frame
    void Update()
    {  
        if (dialogueHandler.currentDialogue != null)
        {
            if (dialogueHandler.currentDialogue.enableDeathScreen)
            {
                music.Stop();
                heartbeatPitch.Stop();
            }
        }
        else
            {
            if (variableHandler.leftLegShot && !variableHandler.bothLegsShot)
            {
                heartbeatPitch.pitch = 1.5f;
            }
            else if (variableHandler.bothLegsShot && !variableHandler.dead)
            {
                heartbeatPitch.pitch = 2f;
            }
            else if (variableHandler.dead)
            {
                heartbeatPitch.pitch = 0f;
            }
        }
        
    }
}
