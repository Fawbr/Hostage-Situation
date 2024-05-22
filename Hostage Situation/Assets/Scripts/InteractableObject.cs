using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public DialoguePath interactDialogue;
    public Material hoverMat;
    public float scale;
    public void HoverDisplay()
    {
        gameObject.layer = 3;
        hoverMat.SetFloat("_Scale", scale);
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 3;
            foreach (Transform childOfChild in child)
            {
                if (childOfChild != null)
                {
                    childOfChild.gameObject.layer = 3;
                }
            }
        }
    }

    public void HideDisplay()
    {
        gameObject.layer = 0;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = 0;
            foreach (Transform childOfChild in child)
            {
                childOfChild.gameObject.layer = 0;
                if (childOfChild != null)
                {
                    childOfChild.gameObject.layer = 0;
                }
            }
        }
    }
}
