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

        // Reset PlayerPrefs p�i prvn�m spu�t�n�
        if (!PlayerPrefs.HasKey("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", 1);
            PlayerPrefs.Save();
        }

        // Na�t�te po�et odemknut�ch �rovn�
        unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked");

        // Nastavte tla��tka
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

