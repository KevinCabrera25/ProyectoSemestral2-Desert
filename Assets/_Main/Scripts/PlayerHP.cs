using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    // Variable for the Player's HP
    [SerializeField] private float _playerHP = default;
    // Reference to assign the GAME OVER SCREEN
    // [SerializeField] private GameOverScreen GameOverScreen = default;
    // Reference to assign the HP UI
    [SerializeField] private TextMeshProUGUI _playerHPUI = default;
    // Reference to assign the Health Bar Array
    [SerializeField] private Image[] _healthBars;

    // This Method accepts the Amount of Player's Damage Taken
    public void PlayerDamageTaken(float amountPDT)
    {
        // Substracting Player's Life
        _playerHP -= amountPDT;
        if (_playerHP < 0)
        {
            _playerHP = 0;
        }

        // Instantiate the Audio Manager for the Grunt Sounds
        AudioManager.Instance.PlayDamageTaken();

        // Updating the UI
        _playerHPUI.SetText("HP: " + _playerHP);

        // Loop to detect the images of the HP Bar Array
        for (int i = 0; i < _healthBars.Length; i++)
        {
            // Displays the HP Bar depending on the HP 
            _healthBars[i].enabled = !DisplayHealthBars(_playerHP, i);
        }

        // Condition to Dead Player
        if (_playerHP <= 0)
        {
            // Invokes the GameOver Method
            GameOver();
        }
    }

    // Method that takes the HP value and pointNumber
    private bool DisplayHealthBars(float _health, int _pointNumber)
    {
        // If the pointNumber*10 is greater or equal to the HP then the bool is TRUE
        return ((_pointNumber * 10) >= _health);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        // Invokes the Method Screen from the GameOverScreen Script
        // GameOverScreen.Screen();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MedKit"))
        {
            // Instantiate the Audio Manager for the Healing Sound
            AudioManager.Instance.PlayHealing();
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
