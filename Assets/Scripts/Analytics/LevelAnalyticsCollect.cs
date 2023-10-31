using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelAnalyticsCollect : MonoBehaviour
{
    // The URL is needed to hook up the Google Form.
    private string URL;
    
    // The level that we are collecting analytics on.
    private int level;
    
    // The metrics to be collected for Level One
    private int _timeTaken;
    private int _timeTravelCnt;
    private bool _levelComplete;
    
    // Create a unique session ID for each run of Level One
    private void Awake()
    {
        _timeTravelCnt = 0;
        _levelComplete = false;
        if (SceneManager.GetActiveScene().name == "Level_1_liu")
        {
            level = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Level2Lg")
        {
            level = 2;
        }
    }
    
    // Submit the data collected to Google Form
    public void Send()
    {
        StartCoroutine(Post(level, _timeTaken.ToString(), _timeTravelCnt.ToString(), _levelComplete.ToString()));
    }

    public void UpdateTime(float tt)
    {
        _timeTaken = Mathf.RoundToInt(tt);
    }

    public void UpdateComplete()
    {
        _levelComplete = true;
    }

    public void UpdateTravel()
    {
        _timeTravelCnt++;
    }
    
    // The POST method
    private IEnumerator Post(int level, string timeTaken, string timeTravelCnt, string levelComplete)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        
        if (level == 1)
        {
            URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSe9ZGZINblJS4p9lUaTWsM7Atgwk1hcbzMO11oidGpgLOyo7g/formResponse";
            // form.AddField("entry.1650174418", timeTaken);
            form.AddField("entry.2111983385", timeTravelCnt);
            form.AddField("entry.280124455", levelComplete);
        }
        else if (level == 2)
        {
            URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSdRVdOWY7xnJhP4R9jgyWB9lwcn9oQrwwJN3WJ_-xsgNeiQsw/formResponse";
            form.AddField("entry.1438990754", timeTravelCnt);
            form.AddField("entry.1594580052", levelComplete);
        }
        
        // Send responses and verify result
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Level" + level + ": " + "Form upload complete!");
            }
        }
    }
}
