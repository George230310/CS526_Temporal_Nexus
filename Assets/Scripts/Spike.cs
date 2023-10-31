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
            comp.TakeDamage(10f);

            canTakeDamage = false; // Disable damage temporarily.
            StartCoroutine(EnableDamageAfterCooldown());

            //StartCoroutine(ShakePlayer(other.transform, 0.1f, 0.1f, 0.1f));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 otherPosition = other.gameObject.transform.position;
            
            HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
            comp.TakeDamage(20f);

            // Call the ShakePlayer method to make the player vibrate.
            StartCoroutine(ShakePlayer(other.transform, 0.1f, 0.1f, 0.1f));
            if (SceneManager.GetActiveScene().name == "Level_1_liu")
            {
                StartCoroutine(Post(1, otherPosition.x.ToString(), otherPosition.y.ToString()));
            }
            else if (SceneManager.GetActiveScene().name == "Level2Lg")
            {
                StartCoroutine(Post(2, otherPosition.x.ToString(), otherPosition.y.ToString()));
            }
            playerHealth = null; // Player is no longer in the spike area.
        }
    }

    IEnumerator EnableDamageAfterCooldown()
    {
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true; // Enable damage after the cooldown.
    }

    IEnumerator DamageOverTime()
    {
        while (playerHealth != null)
        {
            playerHealth.TakeDamage(5f); // Adjust the damage per 2 seconds as needed.
            yield return new WaitForSeconds(2f); // Damage rate is every 2 seconds.
        }
    }

    [CanBeNull]
    IEnumerator ShakePlayer(Transform playerTransform, float duration, float magnitudeX, float magnitudeY)
    {
        Vector3 originalPosition = playerTransform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float xOffset = Random.Range(-3f, 3f) * magnitudeX;
            float yOffset = Random.Range(-3f, 3f) * magnitudeY;

            playerTransform.position = originalPosition + new Vector3(xOffset, yOffset, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = originalPosition;
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
