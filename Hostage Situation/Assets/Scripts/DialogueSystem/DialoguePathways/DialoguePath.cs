using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu()]
public class DialoguePath : ScriptableObject
{
    public List<string> dialogue = new List<string>();
    public List<Speaker> speaker = new List<Speaker>();
    public List<GameObject> poses = new List<GameObject>();
    public List<AudioClip> soundEffects = new List<AudioClip>();
    public ChoiceOption choice;

    public bool startFight;
    public bool startInterview;
    public bool resetAtEnd;
    public bool transitionToInterview;
    public int lineIndex = 0;
    public int increaseFailure;
    public bool enableGunPath;
    public bool shootLeg;
    public bool enableDeathScreen;
    public bool updatedBools;
}
