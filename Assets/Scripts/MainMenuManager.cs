using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string StartingSceneName;

    public GameObject HelpPanel;

    public void Play()
    {
        SceneManager.LoadScene(StartingSceneName);
    }

    public void Help()
    {
        HelpPanel.SetActive(true);
    }

    public void DismissHelp()
    {
        HelpPanel.SetActive(false);
    }
}
