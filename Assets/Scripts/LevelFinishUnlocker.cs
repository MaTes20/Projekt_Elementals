using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishUnlocker : MonoBehaviour
{
    public int levelToUnlock;
    int numberOfUnlockedLevels;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            numberOfUnlockedLevels = PlayerPrefs.GetInt("levelsUnlocked");

            if (numberOfUnlockedLevels < levelToUnlock)
            {
                PlayerPrefs.SetInt("levelsUnlocked", levelToUnlock);
                PlayerPrefs.Save(); // Uložení zmìn
            }
        }
    }
}
