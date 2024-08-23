using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockHandler : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    int unlockedLevelsNumber;

    private void Start()
    {
        // Reset PlayerPrefs jen jednou pøi startu hry
        if (!PlayerPrefs.HasKey("hasResetLevels"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("levelsUnlocked", 1);
            PlayerPrefs.SetInt("hasResetLevels", 1);
            PlayerPrefs.Save();
        }

        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

    }

    private void Update()
    {
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");

        for (int i = 0; i < unlockedLevelsNumber; i++)
        {
            if (i < unlockedLevelsNumber)
            {
                buttons[i].interactable = true;
            }
        }

    }

  

}

