using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu()]
public class DialoguePath : ScriptableObject
{
    public List<string> dialogue = new List<string>();
    public List<Speaker> speaker = new List<Speaker>();
    public ChoiceOption choice;
    public bool resetAtEnd;
    public int lineIndex = 0;
}
