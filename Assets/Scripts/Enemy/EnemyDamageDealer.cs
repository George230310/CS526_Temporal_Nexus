using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EnemyDamageDealer : MonoBehaviour
{
    private string URL;
    private int damage = 50;
    [SerializeField] private float collisionDelta = 1.8f;
    private bool _canBeStomped = true;
    private bool _canDealDamage = true;

    public void ToggleCanBeStomped(bool can)
    {
        _canBeStomped = can;
    }
    
    public void ToggleCanDealDamage(bool can)
    {
        _canDealDamage = can;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // must check collision direction
            Vector3 otherPosition = other.gameObject.transform.position;
            Vector3 myPosition = gameObject.transform.position;

            bool hitFromTop = (otherPosition.y - myPosition.y > collisionDelta);
            
            // if enemy gets hit from top, enemy is stomped
            if (hitFromTop && _canBeStomped)
            {
                HealthComponent myHealth = GetComponent<HealthComponent>();
                if (myHealth)
                {
                    myHealth.TakeDamage(100.0f);
                }
            }
            // else deal damage to player
            else if (_canDealDamage)
            {
                HealthComponent comp = other.gameObject.GetComponent<HealthComponent>();
                if (comp)
                {
                    comp.TakeDamage(damage);
                    if (SceneManager.GetActiveScene().name == "Level_1_liu")
                    {
                        StartCoroutine(Post(1, otherPosition.x.ToString(), otherPosition.y.ToString()));
                    }
                    else if (SceneManager.GetActiveScene().name == "Level2Lg")
                    {
                        StartCoroutine(Post(2, otherPosition.x.ToString(), otherPosition.y.ToString()));
                    }
                }
            }
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
