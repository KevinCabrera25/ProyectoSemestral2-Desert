using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Variable for the Player's HP
    [SerializeField] private float _playerHP = default;
    // Reference to assign the GAME OVER SCREEN
    [SerializeField] private GameOverScreen GameOverScreen = default;
    // Reference to assign the HP UI
    [SerializeField] private TextMeshProUGUI _playerHPUI = default;

    // This Method accepts the Amount of Player's Damage Taken
    public void PlayerDamageTaken(float amountPDT)
    {
        // Substracting Player's Life
        _playerHP -= amountPDT;
        // Updating the UI
        _playerHPUI.SetText("HP: " + _playerHP);

        // Condition to Dead Player
        if (_playerHP <= 0)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MedKit"))
        {
            // Invokes the Coroutine Grab
            StartCoroutine(Grab(other.GetComponent<MeshRenderer>()));
            // Adding Health Points to the Player
            _playerHP += 30;
            // Updating the UI
            _playerHPUI.SetText("HP: " + _playerHP);
            Destroy(other.gameObject);
        }
    }

    // Coroutine to change the colour after interaction with MedKit
    IEnumerator Grab(MeshRenderer otherDraw)
    {
        otherDraw.material.color = Color.green;
        yield return new WaitForSeconds(3f);
    }
}
