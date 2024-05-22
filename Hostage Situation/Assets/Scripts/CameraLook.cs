using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    float x, y;
    [SerializeField] float sensitivity;
    [SerializeField] DialogueHandler dialogueHandler;
    [SerializeField] GameObject focusPoint;
    Vector3 rotate;
    GameObject objectPreviouslyHit;
    [SerializeField] InteractableObject interactable = null;    

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove();
        ObjectScan();
    }

    void CameraMove()
    {
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x * sensitivity, y * -sensitivity, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
    }

    void ObjectScan()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            interactable = hit.transform.gameObject.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                interactable.HoverDisplay();
                objectPreviouslyHit = hit.transform.gameObject;
                if (Input.GetMouseButtonDown(0))
                {
                    ChangeLockState(false);
                    dialogueHandler.currentDialogue = interactable.interactDialogue;
                    dialogueHandler.currentDialogue.lineIndex = 0;
                    interactable.HideDisplay();
                    dialogueHandler.isTyping = false;
                    dialogueHandler.PlayDialogue(dialogueHandler.currentDialogue);
                    Destroy(interactable);
                    Vector3 prevTransform = transform.position;
                    transform.LookAt(Vector3.Lerp(prevTransform, focusPoint.transform.position, 60));
                    this.enabled = false;
                }
            }
            if (hit.transform.gameObject != objectPreviouslyHit)
            {
                InteractableObject[] interactables = FindObjectsOfType<InteractableObject>();
                foreach (InteractableObject interactable in interactables)
                {
                    interactable.HideDisplay();
                }
            }
        }
    }

    public void ChangeLockState(bool lockState)
    {
        if (lockState)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}