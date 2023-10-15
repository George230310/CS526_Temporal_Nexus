using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hp_shower : MonoBehaviour
{
    float intital_length;
    // Start is called before the first frame update
    void Start()
    {
        intital_length = gameObject.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta=new Vector2 (intital_length *PlayerHealth.health*0.1f, gameObject.GetComponent<RectTransform>().rect.height);

        Debug.Log(PlayerHealth.health);
        if (PlayerHealth.health >= 3)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 255, 0);
        }
        else { gameObject.GetComponent<Image>().color = new Color(255, 0, 0); }
    }
}
