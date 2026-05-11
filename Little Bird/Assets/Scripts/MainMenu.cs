using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // loads your game scene
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!"); // only shows in editor
    }
}