using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadNextLevel()
    {
        // Uložení postupu do PlayerPrefs, pokud jste dokonèili level
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked", 1);

        if (currentLevel >= unlockedLevelsNumber)
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
            PlayerPrefs.Save(); // Uloží data do PlayerPrefs
        }

        // Naètení další scény
        SceneManager.LoadScene(currentLevel + 1);
    }

    public void LoadLevelSelectionMenu()
    {
        // Naète scénu s výbìrem levelù
        SceneManager.LoadScene("LevelSelector");
    }

    public void LoadMainMenu()
    {
        // Naète hlavní menu
        SceneManager.LoadScene("MainMenu");
    }
}
