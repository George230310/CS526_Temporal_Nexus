using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelOne : MonoBehaviour
{
    // The URL is needed to hook up the Google Form.
    private string URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSe9ZGZINblJS4p9lUaTWsM7Atgwk1hcbzMO11oidGpgLOyo7g/formResponse";
    
    // The metrics to be collected for Level One
    private long _sessionID = 10;
    private int _timeTaken = 5;
    private int _timeTravelCnt = 10;
    
    // Create a unique session ID for each run of Level One
    private void Awake()
    {
        _sessionID = DateTime.Now.Ticks;
    }
    
    // Submit the data collected to Google Form
    public void Send()
    {
        StartCoroutine(Post(_sessionID.ToString(), _timeTaken.ToString(), _timeTravelCnt.ToString()));
    }  
    
    // The POST method
    private IEnumerator Post(String sessionID, string timeTaken, String timeTravelCnt)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        form.AddField("entry.1501136910", sessionID);
        form.AddField("entry.1650174418", timeTaken);
        form.AddField("entry.2111983385", timeTravelCnt);
        
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
                
                Debug.Log("Form upload complete!");
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
