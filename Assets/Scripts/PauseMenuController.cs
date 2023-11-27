using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public GameObject PauseMenuPanel;

    public GameObject HomeButton;

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
        HomeButton.SetActive(false);
        IsPaused = true;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        HomeButton.SetActive(true);
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

    public void NextLevel(string sceneName)
    {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(sceneName);
    }
}
