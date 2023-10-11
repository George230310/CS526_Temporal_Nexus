using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pet_present : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pet_past.alive)
        {
            //if pet is alive what happen in future
            gameObject.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
        }
        else {
            //if pet is dead what happen in future
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}
