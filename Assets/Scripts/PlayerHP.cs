using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Variable for the Player's HP
    [SerializeField] private float _playerHP;
    // Reference to assign the GAME OVER SCREEN
    public GameOverScreen GameOverScreen;

    // This Method accepts the Amount of Player's Damage Taken
    public void PlayerDamageTaken(float amountPDT)
    {
        // Substracting Player's Life
        _playerHP -= amountPDT;

        // Condition to Dead Player
        if(_playerHP <= 0)
        {
            // Invokes the GameOver Method
            GameOver();
        }
    }

    public void GameOver()
    {
        // Invokes the Method Screen from the GameOverScreen Script
        GameOverScreen.Screen();
    }
}
