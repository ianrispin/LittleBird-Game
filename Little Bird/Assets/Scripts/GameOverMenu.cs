using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Main"); // make sure this matches your game scene name exactly
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}