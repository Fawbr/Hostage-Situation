using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] Light spotlight;
    [SerializeField] float minTime, maxTime;
    [SerializeField] GameObject blanc, blancClose;
    float timer;
    int blancNumber, randomNumber;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
        blancNumber = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            spotlight.enabled = !spotlight.enabled;
            timer = Random.Range(minTime, maxTime);
            randomNumber = Random.Range(1, 10);
            if (spotlight.enabled == true)
            {
                if (randomNumber == blancNumber)
                {
                    blanc.SetActive(true);
                }
                blancClose.SetActive(false);
            }
            if (spotlight.enabled == false)
            {
                if (randomNumber == blancNumber)
                {
                    blancClose.SetActive(true);   
                }
                blanc.SetActive(false);
            }

        }
    }
}
