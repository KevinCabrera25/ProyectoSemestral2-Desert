using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for UI
using UnityEngine.SceneManagement; // for scenes

public class GameOverScreen : MonoBehaviour
{
    public void Screen()
    {
        // Activates the GAME OVER Screen
        gameObject.SetActive(true);
        // Pauses the Game
        Time.timeScale = 0f;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
        // Restarts the Game
        Time.timeScale = 1f;

    }
}
