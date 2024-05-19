using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    MouseLook mouseLook = new MouseLook();
    void Start()
    {
        mouseLook.Init(transform, transform);
    }

    // Update is called once per frame
    void Update()
    {
        mouseLook.LookRotation(transform, transform);
    }
}
