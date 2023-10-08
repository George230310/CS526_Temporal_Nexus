using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject EndGamePanel;

    public void EndGame()
    {
        Time.timeScale = 0f;
        EndGamePanel.SetActive(true);
    }

    public void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
