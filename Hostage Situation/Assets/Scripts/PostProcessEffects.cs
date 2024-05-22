using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProcessEffects : MonoBehaviour
{
    [SerializeField] Volume postProcessVolume;
    [SerializeField] VariableHandler variableHandler;

    Color red = new Color(255f, 0f, 0f);

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (variableHandler.leftLegShot && !variableHandler.bothLegsShot)
        {
            if (postProcessVolume.profile.TryGet(out Vignette vignette))
            {
                vignette.color.value = red;
                vignette.intensity.value = Mathf.Sin(0.05f);
            }
        }
        else if (variableHandler.bothLegsShot)
        {
            if (postProcessVolume.profile.TryGet(out Vignette vignette))
            {
                vignette.color.value = red;
                vignette.intensity.value = Mathf.Sin(0.2f);
            }
        }
    }
}
