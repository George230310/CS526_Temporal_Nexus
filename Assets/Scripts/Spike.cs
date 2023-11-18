using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    // The URL is needed to hook up the Google Form.
    private string URL;
    
    private GameManager gameManager;
    private bool canTakeDamage = true;
    private float damageCooldown = 0.5f; // Adjust the cooldown duration as needed.
    private float damageRate = 2f;     // Adjust the rate of damage as needed.
    private HealthComponent playerHealth;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canTakeDamage && other.gameObject.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<HealthComponent>();

            HealthComponent comp = playerHealth;
            comp.TakeDamage(20f);
          
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 otherPosition = other.gameObject.transform.position;
            
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
    


            if (SceneManager.GetActiveScene().name == "Level_1_liu")
            {
                StartCoroutine(Post(1, otherPosition.x.ToString(), otherPosition.y.ToString()));
            }
            else if (SceneManager.GetActiveScene().name == "Level2S")
            {
                StartCoroutine(Post(2, otherPosition.x.ToString(), otherPosition.y.ToString()));
            }
            playerHealth = null; // Player is no longer in the spike area.
        }
    }




    
    private IEnumerator Post(int level, string xCoord, string yCoord)
    {
        WWWForm form = new WWWForm();
        
        if (level == 1)
        {
            URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLScBybnWt0wfTAXkPIyFkR9-fGEl_ef8ySyGkkv_0SrIbmT31g/formResponse";
            form.AddField("entry.960131089", xCoord);
            form.AddField("entry.2122016655", yCoord);
        }
        else if (level == 2)
        {
            URL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLSeoPDLoM-BSJToqyU-03vc3rFrDbq_Bbo5cWCEOF_lyGdpu-g/formResponse";
            form.AddField("entry.738521626", xCoord);
            form.AddField("entry.109999113", yCoord);
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
                Debug.Log("Level" + level + ": " + xCoord + ", "+ yCoord);
            }
        }
    }
}
