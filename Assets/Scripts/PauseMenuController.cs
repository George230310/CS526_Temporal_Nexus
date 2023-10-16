using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject PauseMenuPanel;

    public bool IsPaused;

    public string MainMenuSceneName;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        // Something has already paused, skip.
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        
        Time.timeScale = 0f;
        PauseMenuPanel.SetActive(true);
        IsPaused = true;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
