using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public int nextLevelIndex; // Index další úrovnì v Build Settings

    public void CompleteLevel()
    {
        // Získání aktuálního poètu odemèených úrovní
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        // Pokud je další úroveò vyšší než dosud odemèená úroveò, odemkni ji
        if (nextLevelIndex > levelsUnlocked)
        {
            PlayerPrefs.SetInt("levelsUnlocked", nextLevelIndex);
            PlayerPrefs.Save(); // Uložení zmìn do PlayerPrefs
        }

        // Následnì pøejdi do hlavního menu nebo výbìru úrovní
        SceneManager.LoadScene("MainMenu"); // Nebo "LevelSelect"
    }
}
