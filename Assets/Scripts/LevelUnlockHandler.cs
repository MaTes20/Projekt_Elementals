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

        // Reset PlayerPrefs pøi prvním spuštìní
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
            PlayerPrefs.Save();
        }

        // Naètìte poèet odemknutých úrovní
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");

        // Nastavte tlaèítka
        UpdateButtonInteractable();
    }

    private void Update()
    {
        // Continuously update button states
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");
        UpdateButtonInteractable();

    }

    private void UpdateButtonInteractable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i < unlockedLevelsNumber;
        }
    }

}

