using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BetaAdditionalAnalyticsCollect : MonoBehaviour
{
    private int _currentLevelNumber = -1;
    private String URL;
    
    private void Start()
    {
        AnalyticsEventSystem.OnTimeTravel += OnTimeTravelDataCollect;
        AnalyticsEventSystem.OnTreeChop += OnTreeChopDataCollect;
        AnalyticsEventSystem.OnPetCollect += OnPetDataCollect;

        if (SceneManager.GetActiveScene().name == "Level_1_liu")
        {
            _currentLevelNumber = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Level2S")
        {
            _currentLevelNumber = 2;
        }
    }

    private void OnTimeTravelDataCollect()
    {
        if (_currentLevelNumber == -1)
        {
            return;
        }

        Vector3 playerPos = GameManager.Instance.player.transform.position;
        float xCoord = playerPos.x;
        float yCoord = playerPos.y;

        StartCoroutine(PostTimeTravelPositionData(_currentLevelNumber, xCoord.ToString(), yCoord.ToString()));
    }

    private void OnTreeChopDataCollect()
    {
        if (_currentLevelNumber == -1)
        {
            return;
        }
        
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        float xCoord = playerPos.x;
        float yCoord = playerPos.y;
        
        StartCoroutine(PostTreeChopPositionData(_currentLevelNumber, xCoord.ToString(), yCoord.ToString()));
    }

    private void OnPetDataCollect(bool isCollected)
    {
        if (_currentLevelNumber == -1)
        {
            return;
        }

        StartCoroutine(PostPetCollectData(_currentLevelNumber, isCollected.ToString()));
    }

    private void OnDisable()
    {
        AnalyticsEventSystem.OnTimeTravel -= OnTimeTravelDataCollect;
        AnalyticsEventSystem.OnTreeChop -= OnTreeChopDataCollect;
        AnalyticsEventSystem.OnPetCollect -= OnPetDataCollect;
    }
    
    private IEnumerator PostTimeTravelPositionData(int level, string xCoord, string yCoord)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        
        if (level == 1)
        {
            URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfND2-r3dTovd-CCtIbBVb7vv4a5r9SAH-8QRPNiqwWJPK_ow/formResponse";
            form.AddField("entry.461184103", xCoord);
            form.AddField("entry.1159330319", yCoord);
        }
        else if (level == 2)
        {
            URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeAPSx8PS_O_5Na01OE-BAqK5c9KHoJiOvOGxVeRx-pjl7CEQ/formResponse";
            form.AddField("entry.516103808", xCoord);
            form.AddField("entry.1562622875", yCoord);
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
                Debug.Log("Level" + level + ": " + "Beta time travel position upload complete!");
            }
        }
    }

    private IEnumerator PostTreeChopPositionData(int level, string xCoord, string yCoord)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        
        if (level == 1)
        {
            URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdBn5wUZpktmj2GwlfNQQl_vm9Yo8qvKu0m2EJ0n_Cd2lOawg/formResponse";
            form.AddField("entry.1316861058", xCoord);
            form.AddField("entry.934187631", yCoord);
        }
        else if (level == 2)
        {
            URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfXE0gTiFxlTpT9CbyjbKMGoVZaRyLItfISw8tvKZd0Ozbvvg/formResponse";
            form.AddField("entry.716910625", xCoord);
            form.AddField("entry.181374395", yCoord);
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
                Debug.Log("Level" + level + ": " + "Beta tree chop position upload complete!");
            }
        }
    }

    private IEnumerator PostPetCollectData(int level, string isCollected)
    {
        // Create the form and enter responses
        WWWForm form = new WWWForm();
        
        if (level == 2)
        {
            URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdC2u1lWqERtObr_ecrAnm8bizA4xEYnNxEBOepu5ou9IEZ9A/formResponse";
            form.AddField("entry.1637197739", isCollected);
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
                Debug.Log("Level" + level + ": " + "Beta pet usage upload complete!");
            }
        }
    }
}
