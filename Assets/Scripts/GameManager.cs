using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject EndGamePanel;
    public GameObject WinGamePanel;

    public void EndGame()
    {
        Time.timeScale = 0f;

        if (EndGamePanel)
        {
            EndGamePanel.SetActive(true);
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0f;

        if (WinGamePanel)
        {
            WinGamePanel.SetActive(true);
        }
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
