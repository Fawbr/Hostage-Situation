using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class BlackScreenPopup : MonoBehaviour
{
    void Update()
    {
        
    }
    public void EnableBlackScreen(GameObject blackScreen, TextMeshProUGUI blackScreenText, DialoguePath dialoguePath)
    {
        blackScreen.SetActive(true);
        blackScreenText.enabled = true;
        StartCoroutine(ScreenTypewriter(dialoguePath, blackScreenText));
    }

    public void DisableBlackScreen(GameObject blackScreen, TextMeshProUGUI blackScreenText)
    {
        blackScreen.SetActive(false);
        blackScreenText.enabled = false;
    }
    IEnumerator ScreenTypewriter(DialoguePath dialoguePath, TextMeshProUGUI dialogueText)
    {
        string joinedString = string.Join("<br><br>", dialoguePath.dialogue);
        print(joinedString);

        if (dialoguePath.resetAtEnd)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }

        foreach (char c in joinedString.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

    }
}
