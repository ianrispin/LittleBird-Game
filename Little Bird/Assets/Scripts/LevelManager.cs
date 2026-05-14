using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // This 'instance' allows other scripts to easily talk to this one
    public static LevelManager instance;

    [Header("Level Settings")]
    [Tooltip("How many enemies need to die to beat the level?")]
    public int requiredKills = 3;
    
    [Tooltip("The exact name of the scene to load next")]
    public string nextSceneName = "Level2";
    
    [Tooltip("How many seconds to wait after the final kill before switching scenes")]
    public float delayBeforeTransition = 2.5f;

    private int currentKills = 0;

    void Awake()
    {
        // Set up the Singleton instance
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKill()
    {
        currentKills++;
        Debug.Log("Kills: " + currentKills + " / " + requiredKills);

        if (currentKills >= requiredKills)
        {
            // Wait a couple seconds so the player can see the final death animation
            Invoke("LoadNextScene", delayBeforeTransition);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}