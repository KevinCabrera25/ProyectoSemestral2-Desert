using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverDeath : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
