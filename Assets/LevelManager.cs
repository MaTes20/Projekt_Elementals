using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadNextLevel()
    {
        // Ulo�en� postupu do PlayerPrefs, pokud jste dokon�ili level
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevelsNumber = PlayerPrefs.GetInt("levelsUnlocked", 1);

        if (currentLevel >= unlockedLevelsNumber)
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
            PlayerPrefs.Save(); // Ulo�� data do PlayerPrefs
        }

        // Na�ten� dal�� sc�ny
        SceneManager.LoadScene(currentLevel + 1);
    }

    public void LoadLevelSelectionMenu()
    {
        // Na�te sc�nu s v�b�rem level�
        SceneManager.LoadScene("LevelSelector");
    }

    public void LoadMainMenu()
    {
        // Na�te hlavn� menu
        SceneManager.LoadScene("MainMenu");
    }
}
