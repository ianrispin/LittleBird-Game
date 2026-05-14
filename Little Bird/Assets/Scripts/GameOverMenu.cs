using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    void Start()
{
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}
    public void Restart()
    {
        SceneManager.LoadScene("Main"); // make sure this matches your game scene name exactly
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}