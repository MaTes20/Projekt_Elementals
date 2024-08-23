using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public int nextLevelIndex; // Index dal�� �rovn� v Build Settings

    public void CompleteLevel()
    {
        // Z�sk�n� aktu�ln�ho po�tu odem�en�ch �rovn�
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        // Pokud je dal�� �rove� vy��� ne� dosud odem�en� �rove�, odemkni ji
        if (nextLevelIndex > levelsUnlocked)
        {
            PlayerPrefs.SetInt("levelsUnlocked", nextLevelIndex);
            PlayerPrefs.Save(); // Ulo�en� zm�n do PlayerPrefs
        }

        // N�sledn� p�ejdi do hlavn�ho menu nebo v�b�ru �rovn�
        SceneManager.LoadScene("MainMenu"); // Nebo "LevelSelect"
    }
}
